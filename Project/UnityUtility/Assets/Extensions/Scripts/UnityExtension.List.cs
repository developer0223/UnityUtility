// System
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

/// <summary>
/// Extension class for System.Collection.Generic.List<T>
/// </summary>
public static partial class UnityExtension
{
    public static bool ContainsIndex<T>(this List<T> thiz, int index)
    {
        return thiz.Count > 1 && index >= 0 && index <= thiz.Count - 1;
    }

    public static T Get<T>(this List<T> thiz, int index)
    {
        if (thiz.ContainsIndex<T>(index))
        {
            return thiz[index];
        }

        return default;
    }

    public static bool TryGetValue<T>(this List<T> thiz, int index, out T value)
    {
        if (thiz.ContainsIndex(index))
        {
            value = thiz.Get(index);
            return true;
        }

        value = default;
        return false;
    }

    public static T First<T>(this List<T> thiz)
    {
        return thiz.Get(0);
    }

    public static T Last<T>(this List<T> thiz)
    {
        return thiz.Get(thiz.Count - 1);
    }
}