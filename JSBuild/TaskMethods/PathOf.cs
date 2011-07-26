using System;
using System.IO;
using IronJS;
using IronJS.Runtime;

namespace JSBuild.TaskMethods
{
    public class PathOf : IBuildFunction
    {
        public string MethodName
        {
            get { return "pathOf"; }
        }

        public Func<BoxedValue, BoxedValue> GetFunction()
        {
            return GetPathOf;
        }

        private static BoxedValue GetPathOf(BoxedValue path)
        {
            var filePath = TypeConverter.ToString(path);
            var file = new FileInfo(filePath);
            var directory = file.DirectoryName;

            return TypeConverter.ToBoxedValue(directory);
        }
    }
}
