// System
using System;

// Project
using LitJson;

// Alias

public static class LitJsonExtensions
{
    internal static string NullToEmpty(this string value)
    {
        if (value == null)
            value = string.Empty;

        return value;
    }

    internal static string StringValue(this JsonData value, string defaultValue = "")
    {
        string result = defaultValue;
        if (value != null)
            result = value.ToString();

        return result;
    }

    internal static int IntegerValue(this JsonData value, int defaultValue = 1)
    {
        int result = defaultValue;
        if (value != null)
            result = int.Parse(value.ToString());

        return result;
    }

    internal static bool BoolValue(this JsonData value, bool defaultValue = false)
    {
        bool result = defaultValue;
        if (value != null)
            result = (bool)value;

        return result;
    }
}