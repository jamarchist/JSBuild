using IronJS.Hosting;

namespace JSBuild.TaskMethods
{
    /// <summary>
    /// An alias for 'include'. It executes a script.
    /// </summary>
    public class Call : Include
    {
        public Call(CSharp.Context context) : base(context)
        {
        }

        public override string MethodName
        {
            get { return "call"; }
        }
    }
}
