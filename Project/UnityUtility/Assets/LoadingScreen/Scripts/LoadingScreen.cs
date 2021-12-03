// System
using System;
using System.Collections;
using System.Collections.Generic;

// C#
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance = null;

    private static readonly string Path = "Prefabs/UI/LoadingScreen";

    public float rotateSpeed = -150.0f;

    [Header("UI Elements")]
    public Image img_loading = null;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Co_RotateImage());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public static LoadingScreen GetOrCreate()
    {
        LoadingScreen _script = Instance;
        if (!_script)
        {
            GameObject _prefab = Resources.Load(Path) as GameObject;
            GameObject _gameObject = Instantiate(_prefab);
            _script = _gameObject.GetComponent<LoadingScreen>();
        }

        return _script;
    }

    public static void Show()
    {
        GetOrCreate().gameObject.SetActive(true);
    }

    public static void Hide()
    {
        GetOrCreate().gameObject.SetActive(false);
    }

    private IEnumerator Co_RotateImage()
    {
        float z = img_loading.transform.localEulerAngles.z;
        while (true)
        {
            z += rotateSpeed * Time.deltaTime;
            img_loading.transform.localEulerAngles = new Vector3(0, 0, z);

            yield return null;
        }
    }
}
