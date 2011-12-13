using System;
using System.IO;
using IronJS;
using JSBuild.Utility;

namespace JSBuild.TaskMethods
{
    public class Save : IBuildAction
    {
        public string MethodName
        {
            get { return "save"; }
        }

        public Action<BoxedValue> GetAction()
        {
            return TaskFunction;
        }

        private static void TaskFunction(BoxedValue options)
        {
            var destination = options.SimpleProperty<string>("destination");
            var contents = options.SimpleProperty<string>("contents");

            File.WriteAllText(destination, contents);
        }
    }
}