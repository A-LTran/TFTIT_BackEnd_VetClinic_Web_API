using System.ComponentModel;

namespace DAL.Tools
{
    public static class ToolSet
    {
        // If value is DBNull, return type default. Else, return type parsed value.
        public static TResult? ReturnNonDBNull<TResult>(Object value)
        {
            return Convert.IsDBNull(value) ? default : (TResult)value;
        }

    }
    public static class TConverter
    {
        //string t = TConverter.ChangeType<T>(StringValue);
        //Return value in T type
        public static T ChangeType<T>(object value)
        {
            return (T)ChangeType(typeof(T), value);
        }

        // Instanciation d'un TypeConverter pour le type T
        public static object ChangeType(Type t, object value)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(t);
            // Gestion d'erreur => Value.ToString() = "true" or "false" avec type t = bool
            if (t == typeof(bool) && bool.TryParse(value.ToString(), out bool valueBool))
            {
                return valueBool;
            }
            return tc.ConvertFrom(value)!;
        }

        // Ajoute un attribut qui spécifie que, pour le type T, le type TC est le type converter       
        public static void RegisterTypeConverter<T, TC>() where TC : TypeConverter
        {
            TypeDescriptor.AddAttributes(typeof(T), new TypeConverterAttribute(typeof(TC)));
        }
    }
}
