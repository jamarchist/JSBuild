namespace JSBuild.TaskMethods
{
    /// <summary>
    /// A marker interface for a function or action provider
    /// </summary>
    public interface IBuildMethod
    {
        string MethodName { get; }
    }
}