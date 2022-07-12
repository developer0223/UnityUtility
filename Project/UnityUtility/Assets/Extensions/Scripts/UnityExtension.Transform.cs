// Unity
using UnityEngine;

/// <summary>
/// Extension class for UnityEngine.Transform
/// </summary>
public static partial class UnityExtension
{
    public static void DeActivateAllChildren(this Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            parent.GetChild(i).gameObject.SetActive(false);
        }
    }

    public static void DeActivateChildrenWith<T>(this Transform parent, bool includeInactive = false) where T : Component
    {
        T[] components = parent.GetComponentsInChildren<T>(includeInactive);
        for (int i = components.Length - 1; i >= 0; i--)
        {
            components[i].gameObject.SetActive(false);
        }
    }


    public static void DestroyAllChildren(this Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(parent.GetChild(i).gameObject);
        }
    }

    public static void DestroyChildrenWith<T>(this Transform parent, bool includeInactive = false) where T : Component
    {
        T[] components = parent.GetComponentsInChildren<T>(includeInactive);
        for (int i = components.Length - 1; i >= 0; i--)
        {
            GameObject.Destroy(components[i].gameObject);
        }
    }

    public static void DestroyChildrenContainsName(this Transform parent, string containsName)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Transform current = parent.GetChild(i);
            if (current.gameObject.name.Contains(containsName))
            {
                GameObject.Destroy(current.gameObject);
            }
        }
    }
}