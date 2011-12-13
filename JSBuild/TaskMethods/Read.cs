using System;
using System.IO;
using IronJS;
using JSBuild.Utility;

namespace JSBuild.TaskMethods
{
    public class Read : IBuildFunction
    {
        public string MethodName
        {
            get { return "read"; }
        }

        public Func<BoxedValue, BoxedValue> GetFunction()
        {
            return TaskFunction;
        }

        private BoxedValue TaskFunction(BoxedValue options)
        {
            var filename = options.SimpleProperty<string>("file");

            return TypeConverter.ToBoxedValue(File.ReadAllText(filename));
        }
    }
}
