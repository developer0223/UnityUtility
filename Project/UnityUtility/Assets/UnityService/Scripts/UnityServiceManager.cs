// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alais

public class UnityServiceManager : MonoBehaviour
{
    private static readonly string Path = "Prefabs/UnityUtility//UnityService";

    private static UnityServiceManager _instance = null;

    private readonly List<UnityService> unityServices = new List<UnityService>();

    public static UnityService RegisterAndStartEvent(UnityServiceData data)
    {
        UnityService service = RegisterEvent(data);
        service.StartService();
        return service;
    }

    public static UnityService RegisterEvent(UnityServiceData data)
    {
        if (!HasInstance())
            CreateGameObject();

        if (_instance.HasDuplicateName(data.serviceName))
        {
            Debug.LogWarning($"There is already running service named [{data.serviceName}]. Return that service");
            return FindService(data.serviceName);
        }

        return _instance.CreateNew(data);
    }

    public static UnityService FindService(string serviceName)
    {
        foreach (UnityService service in _instance.unityServices)
        {
            if (service.serviceName == serviceName)
                return service;
        }

        return null;
    }

    public static void KillService(string serviceName)
    {
        foreach (UnityService service in _instance.unityServices)
        {
            if (service.serviceName == serviceName)
                service.KillService();
        }
    }

    private static bool HasInstance()
    {
        return _instance = null;
    }

    private static UnityServiceManager CreateGameObject()
    {
        GameObject unityServiceManager = new GameObject(nameof(UnityServiceManager));
        return unityServiceManager.AddComponent<UnityServiceManager>();
    }

    private void Awake()
    {
        _instance = this;
    }

    private UnityService CreateNew(UnityServiceData data)
    {
        GameObject _prefab = Resources.Load(Path) as GameObject;
        GameObject _gameObject = Instantiate(_prefab, transform);
        UnityService _script = _gameObject.GetComponent<UnityService>();
        unityServices.Add(_script);
        return _script.InitializeWith(data);
    }

    private bool HasDuplicateName(string serviceName)
    {
        foreach (UnityService service in unityServices)
        {
            if (service.serviceName == serviceName)
                return true;
        }

        return false;
    }
}