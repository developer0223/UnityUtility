// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alais

public class UnityServiceData
{
    public string serviceName = string.Empty;
    public float executeInterval = 60.0f;
    public Action callback = null;
}