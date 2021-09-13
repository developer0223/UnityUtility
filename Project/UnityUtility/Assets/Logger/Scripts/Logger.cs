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
    private static string FileName = "log.txt";

    private static string Path {
        get {
            return System.IO.Path.Combine(Application.persistentDataPath, FileName);
        }
    }

    public static void Log(object message)
    {
        Debug.Log($"Log appended. message : {message}, Path : {Path}");
        //if (!File.Exists(Path))
        //{
        //    File.CreateText(Path);
        //}

        File.AppendAllText(Path, $"{GetDateTimeString()} | {message}\n");
    }

    public static void Clear()
    {
        if (File.Exists(Path))
        {
            File.Delete(Path);
        }
    }

    public static void Copy(string directory)
    {
        if (IsAccessable(Path))
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.Copy(Path, directory);
        }
    }

    private static bool IsAccessable(string filePath)
    {
        FileStream fileStream = null;
        try
        {
            fileStream = new FileStream(Path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        }
        catch (IOException)
        {
            return false;
        }
        finally
        {
            if (fileStream != null)
                fileStream.Close();
        }

        return true;
    }

    private static string GetDateTimeString()
    {
        return DateTime.Now.ToString("yyyy/MM/dd HH:mm.ff");
    }
}