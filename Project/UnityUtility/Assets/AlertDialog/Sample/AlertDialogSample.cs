// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class AlertDialogSample : MonoBehaviour
{
    private void Start()
    {
        AlertDialog.Builder builder = new AlertDialog.Builder()
            .SetTitleText("Alert")
            .SetContentText("This is alertDialog test script.")
            .SetOkButton("Ok", () =>
            {
                Debug.Log($"AlertDialog Ok button clicked");
            })
            .SetCancelButton("Cancel", () =>
            {
                Debug.Log($"AlertDialog Cancel button clicked");
            })
            .Show();
    }
}