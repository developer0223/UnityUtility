using System.Diagnostics;

internal static class Debug
{
//#if LOG_ENABLED
    public const string ENABLE_LOG = "ENABLE_LOG";
//#endif

    [Conditional(ENABLE_LOG)]
    public static void Log(object content, bool writeToFile = true)
    {
        UnityEngine.Debug.Log(content);
        if (writeToFile)
            Logger.Append(content.ToString());
    }

    [Conditional(ENABLE_LOG)]
    public static void LogWarning(object content, bool writeToFile = true)
    {
        UnityEngine.Debug.LogWarning(content);
        if (writeToFile)
            Logger.Append(content.ToString());
    }

    [Conditional(ENABLE_LOG)]
    public static void LogError(object content, bool writeToFile = true)
    {
        UnityEngine.Debug.LogError(content);
        if (writeToFile)
            Logger.Append(content.ToString());
    }
}