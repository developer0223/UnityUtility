// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class FPUtility
{
    public static void For(int iteration, Action callback, int start = 0, int increase = 1)
    {
        for (int i = start; i < iteration; i += increase)
        {
            callback?.Invoke();
        }
    }

    public static void For(int iteration, Action<int> callback, int start = 0, int increase = 1)
    {
        for (int i = start; i < iteration; i += increase)
        {
            callback?.Invoke(i);
        }
    }
}