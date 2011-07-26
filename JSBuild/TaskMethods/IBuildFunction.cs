using System;
using IronJS;
using IronJS.Runtime;

namespace JSBuild.TaskMethods
{
    public interface IBuildFunction : IBuildMethod
    {
        Func<BoxedValue, BoxedValue> GetFunction();
    }
}