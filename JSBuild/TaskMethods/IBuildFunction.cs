using System;
using IronJS;

namespace JSBuild.TaskMethods
{
    public interface IBuildFunction : IBuildMethod
    {
        Func<BoxedValue, BoxedValue> GetFunction();
    }
}