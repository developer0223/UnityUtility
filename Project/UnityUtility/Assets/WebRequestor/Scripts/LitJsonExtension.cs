// Project
using LitJson;

// Alias

public static class LitJsonExtensions
{
    public static string NullToEmpty(this string value)
    {
        if (value == null)
            value = string.Empty;

        return value;
    }

    public static string NullOrStringEmpty(this JsonData value)
    {
        string result = string.Empty;
        if (value != null)
            result = value.ToString();

        return result;
    }

    public static int NullOrDefaultInt(this JsonData value, int defaultValue = 1)
    {
        int result = defaultValue;
        if (value != null)
            result = int.Parse(value.ToString());

        return result;
    }

    public static bool NullOrBool(this JsonData value, bool defaultValue = false)
    {
        bool result = defaultValue;
        if (value != null)
            result = (bool)value;

        return result;
    }
}