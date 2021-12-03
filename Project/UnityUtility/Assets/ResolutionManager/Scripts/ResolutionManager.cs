// System
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolutionManager : MonoBehaviour
{
    // private readonly variables
    private readonly string Path = "Prefabs/UICamera";

    // public variables
    public int targetWidth = 1080;
    public int targetHeight = 1920;

    // private variables
    private Camera uiCamera = null;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        Initialize();
    }

    private void Initialize()
    {
        SetMainCameraSettings();
        GetOrCreateUICamera();
        SetScreenResolution();
        SetCanvasSettings();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        SetMainCameraSettings();
        GetOrCreateUICamera();
        SetScreenResolution();
        SetCanvasSettings();

        GL.Clear(true, true, Color.black);
    }

    private void SetMainCameraSettings()
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = Color.black;
    }

    private void GetOrCreateUICamera()
    {

        if (uiCamera == null)
        {
            UICamera _uiCamera = FindObjectOfType<UICamera>();
            if (_uiCamera != null)
            {
                uiCamera = _uiCamera.GetComponent<Camera>();
            }
            else
            {
                GameObject _prefab = Resources.Load(Path) as GameObject;
                GameObject _gameObjet = GameObject.Instantiate(_prefab);
                uiCamera = _gameObjet.GetComponent<Camera>();
            }
        }
    }

    private void SetScreenResolution()
    {
        Rect rect = uiCamera.rect;

        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)targetWidth / targetHeight);
        float scaleWidth = 1.0f / scaleHeight;

        if (scaleHeight < 1)
        {
            rect.height = scaleHeight;
            rect.y = (1.0f - scaleHeight) / 2.0f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1.0f - scaleWidth) / 2.0f;
        }

        uiCamera.rect = rect;
    }

    private void SetCanvasSettings()
    {
        List<Canvas> canvases = GetUICanvases();

        for (int i = 0; i < canvases.Count; i++)
        {
            canvases[i].renderMode = RenderMode.ScreenSpaceCamera;
            canvases[i].worldCamera = uiCamera;
        }
    }

    private List<Canvas> GetUICanvases()
    {
        List<Canvas> result = new List<Canvas>();

        Canvas[] targetCanvas = FindObjectsOfType<Canvas>();
        for (int i = 0; i < targetCanvas.Length; i++)
        {
            Canvas current = targetCanvas[i];
            if (current.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                result.Add(current);
            }
        }

        return result;
    }
}