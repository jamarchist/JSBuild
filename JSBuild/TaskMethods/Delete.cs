using System;
using System.Collections.Generic;
using System.IO;
using IronJS;

namespace JSBuild.TaskMethods
{
    public class Delete : IBuildAction
    {
        public static void TaskFunction(BoxedValue options)
        {
            var paths = options.ComplexProperty("Paths").ToArray<string>();
            var numberOfRetries = options.SimpleProperty<double>("NumberOfRetries");
            
            System.Console.WriteLine(numberOfRetries);
            TaskFunction(paths);
        }

        private static void TaskFunction(IEnumerable<string> paths)
        {
            foreach (var file in paths)
            {
                System.Console.WriteLine("Deleting {0}", file);
                File.Delete(file);
            }
        }

        public string MethodName
        {
            get { return "deleteFiles"; }
        }

        public Action<BoxedValue> GetAction()
        {
            return TaskFunction;
        }
    }
}