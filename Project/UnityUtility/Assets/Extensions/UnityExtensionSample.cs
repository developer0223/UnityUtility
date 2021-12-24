// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class UnityExtensionSample : MonoBehaviour
{
    [Header("UnityExtension.Text")]
    public Text txt_with_color = null;
    public Text txt_with_size = null;
    public Text txt_with_bold = null;
    public Text txt_with_italic = null;

    private void Start()
    {
        TextExamples();
    }

    private void TextExamples()
    {
        txt_with_color.text = txt_with_color.text.WithColor(UnityExtension.TextColor.red);
        txt_with_size.text = txt_with_size.text.WithSize(25);
        txt_with_bold.text = txt_with_bold.text.WithBold();
        txt_with_italic.text = txt_with_italic.text.WithItalic();
    }
}