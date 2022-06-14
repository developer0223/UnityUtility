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
    private static readonly string Path = "UI/Dialog/AlertDialog";

    [Header("Content")]
    public RectTransform rect_tr_content_area = null;

    [Header("Backgrounds")]
    public Image img_title_background = null;
    public Image img_content_background = null;

    [Header("Texts")]
    public Text txt_title = null;
    public Text txt_content = null;

    [Header("Button (Cancel)")]
    private UnityAction okButtonCallback = null;
    public Button btn_cancel = null;
    public Text txt_button_cancel = null;

    [Header("Button (OK)")]
    private UnityAction cancelButtonCallback = null;
    public Button btn_ok = null;
    public Text txt_button_ok = null;

    private static AlertDialog Create()
    {
        GameObject _prefab = Resources.Load(Path) as GameObject;
        _prefab = Instantiate(_prefab);
        _prefab.SetActive(false);
        _prefab.name = nameof(AlertDialog);

        AlertDialog _script = _prefab.GetComponent<AlertDialog>();
        _script.GetComponent<Canvas>().SetOnTop();

        return _script;
    }

    private void Awake()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        btn_ok.onClick.AddListener(OnOkButtonClicked);
        btn_cancel.onClick.AddListener(OnCancelButtonClicked);
    }

    private void OnOkButtonClicked()
    {
        okButtonCallback?.Invoke();
        Destroy(this.gameObject);
    }

    private void OnCancelButtonClicked()
    {
        cancelButtonCallback?.Invoke();
        Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        BackScreenWrapper.Show();
    }

    private void OnDestroy()
    {
        if (FindObjectsOfType<AlertDialog>().Length == 0)
            BackScreenWrapper.Hide();
    }

    public class Builder
    {
        private AlertDialog dialog = null;

        public Builder()
        {
            dialog = Create();
            SetCancelButtonDisalbed();
        }

        public Builder(AlertDialog dialog)
        {
            if (this.dialog)
                GameObject.Destroy(dialog);

            this.dialog = dialog;
        }

        #region Title Area
        public Builder SetTitleBarBackgroundColor(Color newColor)
        {
            dialog.img_title_background.color = newColor;
            return this;
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

        public Builder SetTitleTextStyle(FontStyle newFontStyle)
        {
            dialog.txt_title.fontStyle = newFontStyle;
            return this;
        }
        #endregion

        #region Content Area
        public Builder SetContentBackgroundColor(Color newColor)
        {
            dialog.img_content_background.color = newColor;
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

        public Builder SetContentTextFontStyle(FontStyle newFontStyle)
        {
            dialog.txt_content.fontStyle = newFontStyle;
            return this;
        }

        public Builder SetContentColor(Color newColor)
        {
            dialog.txt_content.color = newColor;
            return this;
        }
        #endregion

        #region Button Area (OK)
        public Builder SetOkButton(string text, UnityAction callback)
        {
            dialog.txt_button_ok.text = text;
            dialog.okButtonCallback = callback;
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

        public Builder SetOkButtonTextFontStyle(FontStyle newFontStyle)
        {
            dialog.txt_button_ok.fontStyle = newFontStyle;
            return this;
        }

        public Builder SetOkButtonBackgroundColor(Color newColor)
        {
            dialog.btn_ok.image.color = newColor;
            return this;
        }
        #endregion

        #region Button Area (Cancel)
        public Builder SetCancelButton(string text, UnityAction callback)
        {
            SetCancelButtonEnabled();
            dialog.txt_button_cancel.gameObject.SetActive(true);
            dialog.txt_button_cancel.text = text;
            dialog.cancelButtonCallback = callback;
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

        public Builder SetCancelButtonTextFontStyle(FontStyle newFontStyle)
        {
            dialog.txt_button_cancel.fontStyle = newFontStyle;
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
        #endregion

        private void SetCancelButtonEnabled()
        {
            dialog.btn_cancel.gameObject.SetActive(true);

            float width = dialog.rect_tr_content_area.rect.width;
            float height = dialog.txt_button_ok.rectTransform.rect.height;

            Debug.Log($"SetCancelButtonEnabled. width: {width}, height: {height}");

            dialog.btn_ok.GetComponent<RectTransform>().sizeDelta = new Vector2(width / 2, height);
            dialog.btn_cancel.GetComponent<RectTransform>().sizeDelta = new Vector2(width / 2, height);
        }

        private void SetCancelButtonDisalbed()
        {
            float width = dialog.rect_tr_content_area.rect.width;
            float height = dialog.txt_button_ok.rectTransform.rect.height;

            Debug.Log($"SetCancelButtonDisalbed. width: {width}, height: {height}");

            dialog.btn_cancel.gameObject.SetActive(false);
            dialog.btn_ok.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        }
    }
}