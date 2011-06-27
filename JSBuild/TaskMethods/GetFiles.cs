using System;
using System.IO;
using IronJS;
using IronJS.Hosting;
using JSBuild.Utility;

namespace JSBuild.TaskMethods
{
    public class GetFiles : IBuildFunction
    {
        private readonly CSharp.Context context;

        public GetFiles(CSharp.Context context)
        {
            this.context = context;
        }

        public string MethodName
        {
            get { return "getFiles"; }
        }

        public Func<BoxedValue, BoxedValue> GetFunction()
        {
            return GetDirectoryFiles;
        }

        private BoxedValue GetDirectoryFiles(BoxedValue options)
        {
            var directoryPath = options.SimpleProperty<string>("directory");

            var searchPattern = "*.*";
            if (options.Has("pattern")) searchPattern = options.SimpleProperty<string>("pattern");

            var recurse = SearchOption.TopDirectoryOnly;
            if (options.Has("recurse")) recurse = SearchOption.AllDirectories;

            var files = Directory.GetFiles(directoryPath, searchPattern, recurse);

            return files.ToBoxedValue(context.Environment);
        }
    }
}
