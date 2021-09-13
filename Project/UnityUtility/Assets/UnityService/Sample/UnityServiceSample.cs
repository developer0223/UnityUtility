// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alais

public class UnityServiceSample : MonoBehaviour
{
    private int count = 0;

    private void Start()
    {
        UnityService service = UnityServiceManager.RegisterAndStartEvent(new UnityServiceData()
        {
            serviceName = "DebugThreeSeconds",
            executeInterval = 1.0f,
            callback = () =>
            {
                Debug.Log($"{count++}");
                if (count > 3)
                {
                    Debug.Log("Finish!");
                    UnityServiceManager.KillService("DebugThreeSeconds");
                }
            }
        });
    }
}