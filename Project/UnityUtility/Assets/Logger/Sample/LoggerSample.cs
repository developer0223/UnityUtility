// Unity
using UnityEngine;
using UnityEngine.UI;

public class LoggerSample : MonoBehaviour
{
    /// <summary>
    /// You must add [ENABLE_LOG] at Project Settings - Other Settings - Script Compilation - Scripting Define Symbols to enable editor and file logging.
    /// </summary>

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
            Debug.Log(message);
        }
    }

    private bool IsNullOrEmpty(string text)
    {
        text = text.Trim();
        return text == string.Empty && text == null;
    }
}