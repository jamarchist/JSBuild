using System;
using System.IO;
using IronJS;
using IronJS.Runtime;
using JSBuild.Utility;

namespace JSBuild.TaskMethods
{
    public class Copy : IBuildAction
    {
        public static void TaskFunction(BoxedValue options)
        {
            var source = options.ComplexProperty("sources").ToArray<string>();
            var destination = options.ComplexProperty("destinations").ToArray<string>();

            TaskFunction(source, destination);
        }

        private static void TaskFunction(string[] sourceFiles, string[] destinationFiles)
        {
            foreach (var file in sourceFiles)
            {
                var sourceFileIndex = System.Array.IndexOf(sourceFiles, file);
                File.Copy(file, destinationFiles[sourceFileIndex], true);
            }
        }

        public string MethodName
        {
            get { return "copy"; }
        }

        public Action<BoxedValue> GetAction()
        {
            return TaskFunction;
        }
    }
}
