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
        //TextExamples();
        CollectionExample();
    }

    private void TextExamples()
    {
        txt_with_color.text = txt_with_color.text.WithColor(UnityExtension.TextColor.red);
        txt_with_size.text = txt_with_size.text.WithSize(25);
        txt_with_bold.text = txt_with_bold.text.WithBold();
        txt_with_italic.text = txt_with_italic.text.WithItalic();
    }

    private void CollectionExample()
    {
        List<int> intList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        int first = intList.First();
        int get3 = intList.Get(3);
        int get15 = intList.Get(15);

        Debug.Log($"fist: {first}");
        Debug.Log($"get3: {get3}");
        Debug.Log($"get15: {get15}");
    }
}