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
            Logger.Append(DFAULT_TAG, content.ToString(), LogLevel.Debug);
    }

    [Conditional(ENABLE_LOG)]
    public static void Log(string tag, object content, bool writeToFile = false)
    {
        UnityEngine.Debug.Log(content);
        if (writeToFile)
            Logger.Append(tag, content.ToString(), LogLevel.Debug);
    }

    [Conditional(ENABLE_LOG)]
    public static void LogWarning(object content, bool writeToFile = false)
    {
        UnityEngine.Debug.LogWarning(content);
        if (writeToFile)
            Logger.Append(DFAULT_TAG, content.ToString(), LogLevel.Warning);
    }

    [Conditional(ENABLE_LOG)]
    public static void LogWarning(string tag, object content, bool writeToFile = false)
    {
        UnityEngine.Debug.LogWarning(content);
        if (writeToFile)
            Logger.Append(tag, content.ToString(), LogLevel.Warning);
    }

    [Conditional(ENABLE_LOG)]
    public static void LogError(object content, bool writeToFile = false)
    {
        UnityEngine.Debug.LogError(content);
        if (writeToFile)
            Logger.Append(DFAULT_TAG, content.ToString(), LogLevel.Error);
    }

    [Conditional(ENABLE_LOG)]
    public static void LogError(string tag, object content, bool writeToFile = false)
    {
        UnityEngine.Debug.LogError(content);
        if (writeToFile)
            Logger.Append(tag, content.ToString(), LogLevel.Error);
    }
}