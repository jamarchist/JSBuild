using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using IronJS;
using JSBuild.TaskMethods;
using System.Linq;

namespace JSBuild
{
    class Program
    {
        static void Main(string[] args)
        {
            // Context Creation
            var jsContext = new IronJS.Hosting.CSharp.Context();
            jsContext.CreatePrintFunction();

            // Global Functions (in this case, 'include' and 'call' have the same implementation)
            var include = new Include(jsContext);
            var includeFunction = include.GetAction();
            var nativeInclude = IronJS.Native.Utils.CreateFunction(jsContext.Environment, 1, includeFunction);
            jsContext.SetGlobal(include.MethodName, nativeInclude);
            jsContext.SetGlobal("call", nativeInclude);

            // Task Methods
            var jsbuild = jsContext.Environment.NewObject();
            RegisterTaskMethods(jsbuild);
            jsContext.SetGlobal("JSBuild", jsbuild);

            // Execute the specified file, defaulting to 'Default.js'
            jsContext.ExecuteFile(args.Length > 0 ? args[0] : "Default.js");

            Console.ReadKey();
        }

        private static void RegisterTaskMethods(CommonObject jsbuild)
        {
            var buildActions = BuildAllSimple<IBuildAction>();
            var buildFunctions = BuildAllSimple<IBuildFunction>();

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
            return GetTypesImplementing<T>().Where(t => t.GetConstructor(Type.EmptyTypes) != null).Select(
                t => Activator.CreateInstance(t)).Cast<T>();
        }
    }
}
