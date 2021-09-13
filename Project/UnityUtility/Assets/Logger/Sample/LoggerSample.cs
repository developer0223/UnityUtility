// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class LoggerSample : MonoBehaviour
{
    public InputField inputField_message = null;
    public Button button_append = null;

    private void Awake()
    {
        SetListeners();
    }

    private void SetListeners()
    {
        button_append.onClick.AddListener(AppendLog);
    }

    public void AppendLog()
    {
        string message = inputField_message.text;
        if (!IsNullOrEmpty(message))
        {
            Logger.Log(message);
        }
    }

    private bool IsNullOrEmpty(string text)
    {
        text = text.Trim();
        return text == string.Empty && text == null;
    }
}