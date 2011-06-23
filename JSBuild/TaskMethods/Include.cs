using System;
using IronJS;
using IronJS.Hosting;

namespace JSBuild.TaskMethods
{
    /// <summary>
    /// Executes a script from another script.
    /// </summary>
    public class Include : IBuildAction
    {
        private readonly CSharp.Context context;

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
            var file = TypeConverter.ToString(filename);
            context.ExecuteFile(file);
        }
    }
}
