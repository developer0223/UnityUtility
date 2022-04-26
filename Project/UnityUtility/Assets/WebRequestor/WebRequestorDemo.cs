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
    using LitJson;
    // Alias

    public class WebRequestorDemo : MonoBehaviour
    {
        private void Start()
        {
            WebRequestor.Get("http://3.34.139.34:8080/CP/table", new Dictionary<string, string>(), (resultcode, result) =>
            {
                if (resultcode == ResponseCode.OK)
                {
                    Debug.Log($"result : success | result : {result}");

                    JsonData data = JsonMapper.ToObject(result);
                    int intData = data["data"].IntegerValue();
                    string strData = data["data"].StringValue();
                }
                else
                {
                    Debug.Log($"result : fail | result : {result}");
                }
            });
        }
    }
}