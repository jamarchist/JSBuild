using System.Collections.Generic;
using System.Linq;
using IronJS;

namespace JSBuild.Utility
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

        public static bool Has(this BoxedValue ecmaScriptObject, string propertyName)
        {
            return ecmaScriptObject.Array.Has(propertyName);
        }

        public static BoxedValue ToBoxedValue(this IEnumerable<string> clrEnumerable, Environment env)
        {
            var clrArray = clrEnumerable.ToArray();
            var javascriptArray = new ArrayObject(env, clrArray.Length.Unsigned());
            for (var arrayIndex = 0; arrayIndex < clrArray.Length; arrayIndex++)
            {
                var descriptor = new Descriptor();
                descriptor.Value = TypeConverter.ToBoxedValue(clrArray[arrayIndex]);
                descriptor.HasValue = true;

                javascriptArray.Dense[arrayIndex] = descriptor;
            }

            return TypeConverter.ToBoxedValue(javascriptArray);
        }

        public static uint Unsigned(this int integer)
        {
            return System.Convert.ToUInt32(integer);
        }
    }
}
