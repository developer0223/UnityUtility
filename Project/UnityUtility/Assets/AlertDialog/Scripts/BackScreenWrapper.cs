// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class BackScreenWrapper : MonoBehaviour
{
    private static readonly string Path = "UI/Dialog/BackScreenWrapper";

    public static void Show()
    {
        BackScreenWrapper wrapper = GetOrCreate();
        wrapper.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        BackScreenWrapper wrapper = GetOrCreate();
        wrapper.gameObject.SetActive(false);
    }

    private static BackScreenWrapper GetOrCreate()
    {
        BackScreenWrapper _script = FindObjectOfType<BackScreenWrapper>(true);
        if (_script == null)
        {
            GameObject _prefab = Resources.Load(Path) as GameObject;
            _prefab = Instantiate(_prefab);
            _prefab.name = nameof(BackScreenWrapper);

            _script = _prefab.GetComponent<BackScreenWrapper>();
        }
        
        return _script;
    }
}