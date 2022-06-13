// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// Project
// Alias

public class AlertDialog : MonoBehaviour
{
    private static readonly string Path = "/UI/AlertDialog/AlertDialog";

    [Header("Texts")]
    public Text txt_title = null;
    public Text txt_content = null;

    [Header("Buttons")]
    public Button btn_ok = null;
    public Button btn_cancel = null;
    public Text txt_button_ok = null;
    public Text txt_button_cancel = null;

    private static AlertDialog Create()
    {
        GameObject _prefab = Resources.Load(Path) as GameObject;
        _prefab = Instantiate(_prefab);
        _prefab.SetActive(false);

        AlertDialog _script = _prefab.GetComponent<AlertDialog>();
        _script.GetComponent<Canvas>().SetOnTop();

        return _script;
    }

    private void Awake()
    {
        btn_cancel.gameObject.SetActive(false);
    }

    public class Builder
    {
        private AlertDialog dialog = null;

        public Builder()
        {
            dialog = Create();
        }

        public Builder(AlertDialog dialog)
        {
            if (this.dialog)
                GameObject.Destroy(dialog);

            this.dialog = dialog;
        }

        public Builder SetTitleText(string title)
        {
            dialog.txt_title.text = title;
            return this;
        }

        public Builder SetTitleTextSize(int size)
        {
            dialog.txt_title.fontSize = size;
            return this;
        }

        public Builder SetTitleColor(Color newColor)
        {
            dialog.txt_title.color = newColor;
            return this;
        }

        public Builder SetContentText(string content)
        {
            dialog.txt_content.text = content;
            return this;
        }

        public Builder SetContentTextSize(int size)
        {
            dialog.txt_content.fontSize = size;
            return this;
        }

        public Builder SetContentColor(Color newColor)
        {
            dialog.txt_content.color = newColor;
            return this;
        }

        public Builder SetOkButton(string text, UnityAction callback)
        {
            dialog.txt_button_ok.text = text;
            dialog.btn_ok.onClick.RemoveAllListeners();
            dialog.btn_ok.onClick.AddListener(callback);
            return this;
        }

        public Builder SetOkButtonTextSize(int size)
        {
            dialog.txt_button_ok.fontSize = size;
            return this;
        }

        public Builder SetOkButtonTextColor(Color newColor)
        {
            dialog.txt_button_ok.color = newColor;
            return this;
        }

        public Builder SetOkButtonBackgroundColor(Color newColor)
        {
            dialog.btn_ok.image.color = newColor;
            return this;
        }

        public Builder SetCancelButton(string text, UnityAction callback)
        {
            dialog.txt_button_cancel.gameObject.SetActive(true);
            dialog.txt_button_cancel.text = text;
            dialog.btn_cancel.onClick.RemoveAllListeners();
            dialog.btn_cancel.onClick.AddListener(callback);
            return this;
        }

        public Builder SetCancelButtonTextSize(int size)
        {
            dialog.txt_button_cancel.fontSize = size;
            return this;
        }

        public Builder SetCancelButtonTextColor(Color newColor)
        {
            dialog.txt_button_cancel.color = newColor;
            return this;
        }

        public Builder SetCancelButtonBackgroundColor(Color newColor)
        {
            dialog.btn_cancel.image.color = newColor;
            return this;
        }

        public Builder Show()
        {
            dialog.gameObject.SetActive(true);
            return this;
        }
    }
}