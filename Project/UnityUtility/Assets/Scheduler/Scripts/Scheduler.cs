// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class Scheduler : MonoBehaviour
{
    public static readonly string Path = "Prefabs/UnityUtility/Scheduler";

    public List<Schedule<bool>> scheduleList = new List<Schedule<bool>>();

    public static Scheduler GetOrCreate()
    {
        return new Scheduler();
    }

    public static void Add(Schedule<bool> callback)
    {
        GetOrCreate().scheduleList.Add(callback);
    }
}