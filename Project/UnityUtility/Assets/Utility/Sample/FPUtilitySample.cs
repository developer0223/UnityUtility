// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class FPUtilitySample : MonoBehaviour
{
    private void Start()
    {
        FPUtility.For(iteration: 10, callback: (i) =>
        {
            Debug.Log($"current : {i}");
        });
    }
}