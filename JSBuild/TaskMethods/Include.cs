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

                context.Execute(coffeeConvertedToJS);
            }
            else
            {
                context.ExecuteFile(file);                
            }
        }
    }
}
