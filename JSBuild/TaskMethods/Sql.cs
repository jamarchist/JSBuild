using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IronJS;

namespace JSBuild.TaskMethods
{
    public class Sql : IBuildAction
    {
        public string MethodName
        {
            get { return "sql"; }
        }

        public Action<BoxedValue> GetAction()
        {
            
        }
    }
}
