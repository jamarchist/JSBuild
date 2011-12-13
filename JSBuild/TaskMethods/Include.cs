using System;
using System.IO;
using IronJS;
using IronJS.Hosting;
using IronJS.Runtime;

namespace JSBuild.TaskMethods
{
    /// <summary>
    /// Executes a script from another script.
    /// </summary>
    public class Include : IBuildAction
    {
        private readonly CSharp.Context context;
        private static FunctionObject compile;

        public Include(CSharp.Context context)
        {
            this.context = context;
        }

        public virtual string MethodName
        {
            get { return "include"; }
        }

        public Action<BoxedValue> GetAction()
        {
            return IncludeAction;
        }

        private void IncludeAction(BoxedValue filename)
        {
            if (compile == null)
            {
                context.Execute("var compile = function (src) { return CoffeeScript.compile(src, { bare: true }); };");
                compile = this.context.GetGlobalAs<FunctionObject>("compile");
            }

            var file = TypeConverter.ToString(filename);
            if (file.EndsWith("coffee"))
            {
                var coffeeCode = File.ReadAllText(file);
                var boxedResult = compile.Call(context.Globals, coffeeCode);
                var coffeeConvertedToJS = TypeConverter.ToString(boxedResult);

                Console.WriteLine(coffeeConvertedToJS);

                try
                {
                    context.Execute(coffeeConvertedToJS);
                }
                catch (IronJS.Error.Error error)
                {
                    Console.WriteLine(error.Message);
                    Console.WriteLine(error.StackTrace);

                    if (error.InnerException != null)
                    {
                        Console.WriteLine();
                        Console.WriteLine(error.InnerException.Message);
                        Console.WriteLine(error.InnerException.StackTrace);
                    }
                }
            }
            else
            {
                context.ExecuteFile(file);                
            }
        }
    }
}
