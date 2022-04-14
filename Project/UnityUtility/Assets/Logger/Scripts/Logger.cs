// System
using System;
using System.IO;

// Unity
using UnityEngine;

public class Logger
{
    internal static readonly string LogDefaultFileName = "log_";

    internal static void Append(string tag, string content, LogLevel logLevel = LogLevel.Debug)
    {
        string[] lines = content.Split('\n');
        string emptyTag = GetEmptyString(tag.Length);
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0)
            {
                AppendSingleLine(tag, lines[i].Trim(), logLevel);
                return;
            }

            AppendSingleLine(emptyTag, lines[i].Trim(), logLevel);
        }
    }

    internal static void Clear()
    {
        if (File.Exists(GetOrCreateFilePath()))
        {
            File.Delete(GetOrCreateFilePath());
        }
    }

    private static void AppendSingleLine(string tag, string content, LogLevel logLevel)
    {
        string filePath = GetOrCreateFilePath();
        int lineCount = File.ReadAllLines(filePath).Length;
        FileStream fileStream = new FileStream(filePath, FileMode.Append);
        StreamWriter writer = new StreamWriter(fileStream);
        writer.WriteLine($"{lineCount}     {tag}    {GetCurrentTime()} :: {logLevel} :: {content}");
        writer.Flush();
        writer.Dispose();
        fileStream.Dispose();
    }

    private static string GetOrCreateFilePath()
    {
        string directoryPath = Path.Combine(Application.persistentDataPath, "Log");
        string fileName = $"{LogDefaultFileName}{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
        string filePath = Path.Combine(directoryPath, fileName);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        if (!File.Exists(filePath))
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Append);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.WriteLine($"Log file Created :: {GetCurrentTime()}");
            writer.Flush();
            writer.Dispose();
            fileStream.Dispose();
        }

        return filePath;
    }

    private static string GetCurrentTime()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    private static string GetEmptyString(int length)
    {
        string result = string.Empty;
        for (int i = 0; i < length; i++)
        {
            result += " ";
        }

        return result;
    }
}