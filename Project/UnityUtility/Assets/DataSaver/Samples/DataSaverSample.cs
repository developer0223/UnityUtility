// System
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
using LitJson;
// Alias

public class DataSaverSample : MonoBehaviour
{
    public Text txt_debug = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DataSaver.SaveText("Logs1", "test1", new TestClass("aaa", "bbb"));
            txt_debug.text += $"File Saved at filder Logs1/test1.txt. content: aaa, bbb\n";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DataSaver.SaveText("Logs2", "test2.txt", new TestClass("ccc", "ddd"));
            txt_debug.text += $"File Saved at filder Logs2/test2.txt. content: ccc, ddd\n";
        }
    }

    public class TestClass
    {
        public string a = "aaa";
        public string b = "bbb";

        public TestClass(string a, string b)
        {
            this.a = a;
            this.b = b;
        }
    }
}