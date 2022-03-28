// System
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

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
        File.AppendAllText(filePath, $"{lineCount}     {tag}    {GetCurrentTime()} :: {logLevel} :: {content}\n");
    }

    private static string GetOrCreateFilePath()
    {
        //string directoryPath = Path.Combine(Environment.CurrentDirectory, "Log");
        string directoryPath = Path.Combine(Application.persistentDataPath, "Log");
        string fileName = $"{LogDefaultFileName}{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
        string filePath = Path.Combine(directoryPath, fileName);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        if (!File.Exists(filePath))
        {
            File.AppendAllText(filePath, $"Log file Created :: {GetCurrentTime()}\n");
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