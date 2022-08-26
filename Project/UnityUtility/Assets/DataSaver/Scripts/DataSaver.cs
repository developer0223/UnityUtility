// System
using System;
using System.IO;

// Project
using LitJson;

public class DataSaver
{
    private const string Extension = "txt";

    public static bool SaveText(string fileName, object serializableData)
    {
        string json = JsonMapper.ToJson(serializableData);

        if (!fileName.EndsWith("txt"))
        {
            fileName += fileName.EndsWith(".") ? Extension : $".{Extension}";
        }

        string fullPath = Path.Combine(Environment.CurrentDirectory, $"{GetFormattedDateTimeString()}_{fileName}");
        File.WriteAllText(fullPath, json);


        return true;
    }

    public static bool SaveText(string additionalDirectory, string fileName, object serializableData)
    {
        string json = JsonMapper.ToJson(serializableData);
        string saveDirectory = Path.Combine(Environment.CurrentDirectory, additionalDirectory);

        if (!Directory.Exists(saveDirectory))
        {
            DirectoryInfo creationInfo = Directory.CreateDirectory(saveDirectory);
            if (!creationInfo.Exists)
            {
                Debug.LogWarning($"Cannot create directory ({saveDirectory}).");
                return false;
            }
        }

        if (!fileName.EndsWith("txt"))
        {
            fileName += fileName.EndsWith(".") ? Extension : $".{Extension}";
        }

        string fullPath = Path.Combine(saveDirectory, $"{GetFormattedDateTimeString()}_{fileName}");
        File.WriteAllText(fullPath, json);

        return true;
    }

    private static string GetFormattedDateTimeString()
    {
        return DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss");
    }
}