// System
using System;
using System.Threading;

// Unity
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static readonly Lazy<T> instance = new Lazy<T>(() =>
    {
        T _instance = FindObjectOfType<T>() as T;

        if (_instance == null)
        {
            GameObject _gameObject = new GameObject(typeof(T).FullName);
            _instance = _gameObject.AddComponent<T>() as T;

            DontDestroyOnLoad(_gameObject);
        }

        return _instance;
    }, LazyThreadSafetyMode.ExecutionAndPublication);

    public static T Instance
    {
        get
        {
            return instance.Value;
        }
    }
}