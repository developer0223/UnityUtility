namespace developer0223.WebRequestor
{
    // Project
    using LitJson;

    /// <summary>
    /// Extension methods for LitJson library.
    /// </summary>
    public static class LitJsonExtensions
    {
        internal static string NullToEmpty(this string value)
        {
            if (value == null)
                value = string.Empty;

            return value;
        }

        internal static string GetStringValue(this JsonData value, string defaultValue = "")
        {
            string result = defaultValue;
            if (value != null)
                result = value.ToString();

            return result;
        }

        internal static int GetIntegerValue(this JsonData value, int defaultValue = 1)
        {
            int result = defaultValue;
            if (value != null)
                result = int.Parse(value.ToString());

            return result;
        }

        internal static bool GetBoolValue(this JsonData value, bool defaultValue = false)
        {
            bool result = defaultValue;
            if (value != null)
                result = (bool)value;

            return result;
        }
    }
}