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
    private int index = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnAlertDialog(index++);
    }

    private void SpawnAlertDialog(int index)
    {
        AlertDialog.Builder builder = new AlertDialog.Builder()
            .SetTitleText($"Alert ({index})")
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