using System.Linq;
using IronJS;

namespace JSBuild
{
    public static class BoxedValueExtensions
    {
        public static T[] ToArray<T>(this BoxedValue ecmaScriptObject)
        {
            return ecmaScriptObject.Array.ToArray<T>();
        }

        public static T[] ToArray<T>(this ArrayObject ecmaScriptObject)
        {
            return ecmaScriptObject.Dense.Select(desc => IronJS.TypeConverter.ToClrObject(desc.Value)).Cast<T>().ToArray();    
        }

        public static T SimpleProperty<T>(this BoxedValue ecmaScriptObject, string propertyName)
        {
            return (T) ecmaScriptObject.Array.Members[propertyName];
        }

        public static ArrayObject ComplexProperty(this BoxedValue ecmaScriptObject, string propertyName)
        {
            return (ArrayObject) ecmaScriptObject.Array.Members[propertyName];
        }
    }
}
