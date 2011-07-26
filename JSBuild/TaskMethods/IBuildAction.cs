using System;
using IronJS;
using IronJS.Runtime;

namespace JSBuild.TaskMethods
{
    public interface IBuildAction : IBuildMethod
    {
        Action<BoxedValue> GetAction();
    }
}