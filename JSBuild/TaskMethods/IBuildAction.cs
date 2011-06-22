using System;
using IronJS;

namespace JSBuild.TaskMethods
{
    public interface IBuildAction : IBuildMethod
    {
        Action<BoxedValue> GetAction();
    }
}