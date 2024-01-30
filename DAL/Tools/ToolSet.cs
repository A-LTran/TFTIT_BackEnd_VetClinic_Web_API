namespace DAL.Tools
{
    public static class ToolSet
    {
        public static string ToBinary(this int value)
        {
            byte[] myBinary = BitConverter.GetBytes(value);
            return "0x" + BitConverter.ToString(myBinary).Replace("-", "");
        }

        // If value is DBNull, return type default. Else, return type parsed value.
        public static TResult? ReturnNonDBNull<TResult>(Object value)
        {
            return Convert.IsDBNull(value) ? default : (TResult)value;
        }
    }
}
