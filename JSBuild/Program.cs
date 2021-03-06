﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using IronJS;
using IronJS.Runtime;
using JSBuild.TaskMethods;
using System.Linq;
using JSBuild.Utility;

namespace JSBuild
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Context Creation
                var jsContext = new IronJS.Hosting.CSharp.Context();
                jsContext.CreatePrintFunction();

                // Task Methods
                var jsbuild = jsContext.Environment.NewObject();
                RegisterTaskMethodsWithContext(jsbuild, jsContext);
                RegisterSimpleTaskMethods(jsbuild);
                jsContext.SetGlobal("JSBuild", jsbuild);

                // Automatically-included Scripts
                RunEmbeddedScripts(jsContext);

                if (args.Length > 0)
                {
                    if (String.Compare(args[0], "-repl", true) != 0)
                    {
                        // Execute the specified file, defaulting to 'Default.js'
                        jsContext.ExecuteFile(args.Length > 0 ? args[0] : "Default.js");  
                        Console.ReadKey();                    
                    }
                    else
                    {
                        var shouldContinue = true;
                        while (shouldContinue)
                        {
                            Console.Write("JSBuild> ");
                            var userInput = Console.ReadLine();
                        
                            if (String.Compare(userInput, "exit", true) == 0)
                            {
                                shouldContinue = false;
                            }
                            else
                            {
                                jsContext.Execute(userInput);                            
                            }
                        }
                    }
                }
                else
                {
                    jsContext.ExecuteFile("Default.js");
                }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
        }

        private static void RunEmbeddedScripts(IronJS.Hosting.CSharp.Context context)
        {
            var jsbuild = Assembly.GetExecutingAssembly();
            var resources = jsbuild.GetManifestResourceNames().Select(r => Path.GetFileName(r));

            var extensions = resources.Where(r => r.ScriptName().EndsWith("-extensions"));
            var tasks = resources.Where(r => r.ScriptName().EndsWith("-task"));

            ExecuteScriptCode(extensions, jsbuild, context);
            ExecuteScriptCode(tasks, jsbuild, context);
        }

        private static void ExecuteScriptCode(IEnumerable<string> codeFile, Assembly jsbuild, IronJS.Hosting.CSharp.Context context)
        {
            foreach (var file in codeFile)
            {
                var fileStream = jsbuild.GetManifestResourceStream(file);
                var code = new StreamReader(fileStream).ReadToEnd();

                context.Execute(code);
            }
        }

        private static void RegisterTaskMethodsWithContext(CommonObject jsbuild, IronJS.Hosting.CSharp.Context context)
        {
            RegisterTaskMethods(jsbuild, () => BuildAllRequiringContext<IBuildAction>(context), () => BuildAllRequiringContext<IBuildFunction>(context));
        }

        private static void RegisterSimpleTaskMethods(CommonObject jsbuild)
        {
            RegisterTaskMethods(jsbuild, () => BuildAllSimple<IBuildAction>(), () => BuildAllSimple<IBuildFunction>());
        }

        private static void RegisterTaskMethods(CommonObject jsbuild, Func<IEnumerable<IBuildAction>> createBuildActions, Func<IEnumerable<IBuildFunction>> createBuildFunctions)
        {
            var buildActions = createBuildActions();
            var buildFunctions = createBuildFunctions();

            foreach (var nativeAction in buildActions)
            {
                var jsAction = IronJS.Native.Utils.CreateFunction(jsbuild.Env, 1, nativeAction.GetAction());
                jsbuild.Put(nativeAction.MethodName, jsAction);
            }

            foreach (var nativeFunction in buildFunctions)
            {
                var jsFunction = IronJS.Native.Utils.CreateFunction(jsbuild.Env, 1, nativeFunction.GetFunction());
                jsbuild.Put(nativeFunction.MethodName, jsFunction);
            }
        }

        private static IEnumerable<Type> GetTypesImplementing<TInterface>()
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof (TInterface).IsAssignableFrom(t));
        }

        private static IEnumerable<T> BuildAllSimple<T>()
        {
            return GetTypesImplementing<T>()
                    .Where(t => t.GetConstructor(Type.EmptyTypes) != null)
                    .Select(t => Activator.CreateInstance(t))
                        .Cast<T>();
        }

        private static IEnumerable<T> BuildAllRequiringContext<T>(IronJS.Hosting.CSharp.Context context)
        {
            return GetTypesImplementing<T>()
                    .Where(t => t.GetConstructor(new Type[] {typeof (IronJS.Hosting.CSharp.Context)}) != null)
                    .Select(t => Activator.CreateInstance(t, context))
                        .Cast<T>();
        }
    }
}
