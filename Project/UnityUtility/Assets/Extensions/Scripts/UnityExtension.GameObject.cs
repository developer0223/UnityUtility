// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public static partial class UnityExtension
{
    public static GameObject Instantiate(this GameObject gameObject)
    {
        return GameObject.Instantiate(gameObject);
    }

    public static GameObject Instantiate(this GameObject gameObject, Transform parent)
    {
        return GameObject.Instantiate(gameObject, parent);
    }

    public static T Instantiate<T>(this GameObject gameObject) where T : Component
    {
        gameObject = GameObject.Instantiate(gameObject);
        T component = gameObject.GetComponent<T>();
        if (component == null)
            component = gameObject.AddComponent<T>();
        return component;
    }

    public static T Instantiate<T>(this GameObject gameObject, Transform parent) where T : Component
    {
        gameObject = GameObject.Instantiate(gameObject, parent);
        T component = gameObject.GetComponent<T>();
        if (component == null)
            component = gameObject.AddComponent<T>();
        return component;
    }
}