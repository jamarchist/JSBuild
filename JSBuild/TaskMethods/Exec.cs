using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using IronJS;
using JSBuild;

namespace JSBuild.TaskMethods
{
    public class Exec : IBuildFunction
    {
        public string MethodName
        {
            get { return "exec"; }
        }

        public Func<BoxedValue, BoxedValue> GetFunction()
        {
            var func = (Func<BoxedValue, BoxedValue>) (
                options =>
                {
                    var executable = options.SimpleProperty<string>("Path");
                    var arguments = options.SimpleProperty<string>("Args");

                    var process = new Process();
                    process.StartInfo.FileName = executable;
                    process.StartInfo.Arguments = arguments;
                    process.Start();
                    process.WaitForExit();

                    return TypeConverter.ToBoxedValue(Convert.ToDouble(process.ExitCode));
                });

            return func;
        }
    }
}
