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
}
