namespace developer0223.WebRequestor
{
    // System
    using System;
    using System.Collections;
    using System.Collections.Generic;

    // Unity
    using UnityEngine;
    using UnityEngine.UI;

    // Project
    // Alias

    public class WebRequestorDemo : MonoBehaviour
    {
        private void Start()
        {
            WebRequestor.Get("http://3.34.139.34:8080/CP/table", new Dictionary<string, string>(), (resultcode, result) =>
            {
                if (resultcode == ResultCode.SUCCESS)
                {
                    Debug.Log($"result : success | result : {result}");
                }
                else
                {
                    Debug.Log($"result : fail | result : {result}");
                }
            });
        }
    }
}