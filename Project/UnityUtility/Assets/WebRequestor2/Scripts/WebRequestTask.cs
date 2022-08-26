// System
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class WebRequestTask : Task
{
    public WebRequestTask(Action action) : base(action)
    {

    }
}