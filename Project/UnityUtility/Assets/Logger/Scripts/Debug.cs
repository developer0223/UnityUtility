using System.Diagnostics;

internal static class Debug
{
//#if LOG_ENABLED
    public const string ENABLE_LOG = "ENABLE_LOG";
    //#endif
    public const string DFAULT_TAG = "developer0223";

    [Conditional(ENABLE_LOG)]
    public static void Log(object content, bool writeToFile = false)
    {
        UnityEngine.Debug.Log(content);
        if (writeToFile)
            Logger.Append(content.ToString());
    }

    [Conditional(ENABLE_LOG)]
    public static void LogWarning(object content, bool writeToFile = false)
    {
        UnityEngine.Debug.LogWarning(content);
        if (writeToFile)
            Logger.Append(content.ToString(), LogLevel.Warning);
    }

    [Conditional(ENABLE_LOG)]
    public static void LogError(object content, bool writeToFile = false)
    {
        UnityEngine.Debug.LogError(content);
        if (writeToFile)
            Logger.Append(content.ToString(), LogLevel.Error);
    }
}