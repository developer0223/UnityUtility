// System
using System.Collections.Generic;

/// <summary>
/// Extension class for System.Collection.Generic.Dictionary<TKey, TValue>
/// </summary>
public static partial class UnityExtension
{
    public static bool TryGetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> thiz, TKey key, out TValue value)
    {
        if (thiz.ContainsKey(key))
        {
            value = thiz[key];
            return true;
        }

        value = default;
        return false;
    }
}