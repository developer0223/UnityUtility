// System
using System;
using System.Collections;

// Unity
using UnityEngine;

[Serializable]
public class Schedule
{
    [SerializeField] public string name = string.Empty;
    [SerializeField] public float preDelay = 0.0f;
    [SerializeField] public float postDelay = 0.0f;
    public IEnumerator callback = null;

    public Schedule(string name, float preDelay = 0.0f, float postDelay = 0.0f, IEnumerator callback = null)
    {
        this.name = name;
        this.preDelay = preDelay;
        this.postDelay = postDelay;
        this.callback = callback;
    }
}

//[Serializable]
//public class Schedule
//{
//    [SerializeField] public string name = string.Empty;
//    [SerializeField] public float preDelay = 0.0f;
//    [SerializeField] public float postDelay = 0.0f;
//    public Action callback = null;

//    public Schedule(string name, float preDelay = 0.0f, float postDelay = 0.0f, Action callback = null)
//    {
//        this.name = name;
//        this.preDelay = preDelay;
//        this.postDelay = postDelay;
//        this.callback = callback;
//    }
//}

//[Serializable]
//public class Schedule<T>
//{
//    [SerializeField] public string name = string.Empty;
//    [SerializeField] public float preDelay = 0.0f;
//    [SerializeField] public float postDelay = 0.0f;
//    public Action<T> callback = null;

//    public Schedule(string name, float preDelay = 0.0f, float postDelay = 0.0f, Action<T> callback = null)
//    {
//        this.name = name;
//        this.preDelay = preDelay;
//        this.postDelay = postDelay;
//        this.callback = callback;
//    }
//}

//[Serializable]
//public class Schedule<T1,T2>
//{
//    [SerializeField] public string name = string.Empty;
//    [SerializeField] public float preDelay = 0.0f;
//    [SerializeField] public float postDelay = 0.0f;
//    public Action<T1, T2> callback = null;

//    public Schedule(string name, float preDelay = 0.0f, float postDelay = 0.0f, Action<T1, T2> callback = null)
//    {
//        this.name = name;
//        this.preDelay = preDelay;
//        this.postDelay = postDelay;
//        this.callback = callback;
//    }
//}

//[Serializable]
//public class Schedule<T1, T2, T3>
//{
//    [SerializeField] public string name = string.Empty;
//    [SerializeField] public float preDelay = 0.0f;
//    [SerializeField] public float postDelay = 0.0f;
//    public Action<T1, T2, T3> callback = null;

//    public Schedule(string name, float preDelay = 0.0f, float postDelay = 0.0f, Action<T1, T2, T3> callback = null)
//    {
//        this.name = name;
//        this.preDelay = preDelay;
//        this.postDelay = postDelay;
//        this.callback = callback;
//    }
//}