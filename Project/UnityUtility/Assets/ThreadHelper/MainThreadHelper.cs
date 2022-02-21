// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class MainThreadHelper : MonoBehaviour
{
    // public static class instance
    private static MainThreadHelper Instance = null;

    private Queue<Action> mainThreadTask = new Queue<Action>();

    /// <summary>
    /// Get or create MainThreadHelper instance.
    /// Warning : This method must be executed on main thread.
    /// </summary>
    /// <returns></returns>
    public static MainThreadHelper GetOrCreate()
    {
        if (Instance == null)
        {
            GameObject _gameObject = new GameObject(nameof(MainThreadHelper));
            Instance = _gameObject.AddComponent<MainThreadHelper>();
        }

        return Instance;
    }

    /// <summary>
    /// Add actino to queue
    /// </summary>
    /// <param name="action"></param>
    public static void AddAction(Action action)
    {
        if (action == null)
            return;

        lock (action)
        {
            MainThreadHelper _helper = GetOrCreate();
            _helper.mainThreadTask.Enqueue(action);
        }
    }

    private void Update()
    {
        while (mainThreadTask.Count >= 1)
        {
            mainThreadTask.Dequeue()?.Invoke();
        }
    }
}