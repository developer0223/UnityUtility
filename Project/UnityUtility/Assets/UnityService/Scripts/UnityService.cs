// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alais

public class UnityService : MonoBehaviour
{
    [HideInInspector] public string serviceName = string.Empty;
    [HideInInspector] public float executeInterval = 60.0f;
    [HideInInspector] public Action callback = null;

    private WaitForSeconds waitForSeconds = null;
    private Coroutine serviceBody = null;

    public UnityService InitializeWith(UnityServiceData data)
    {
        serviceName = data.serviceName;
        executeInterval = data.executeInterval;
        callback = data.callback;

        ChangeExecuteInterval(executeInterval);

        return this;
    }

    public void StartService()
    {
        serviceBody = StartCoroutine(ServiceBody());
    }

    public void StopService()
    {
        if (serviceBody != null) StopCoroutine(serviceBody);
    }

    public void KillService()
    {
        StopAllCoroutines();
        Destroy(this.gameObject);
    }

    public void ChangeExecuteInterval(float newInterval)
    {
        waitForSeconds = new WaitForSeconds(newInterval);
    }

    private IEnumerator ServiceBody()
    {
        while (true)
        {
            callback?.Invoke();
            yield return waitForSeconds;
        }
    }
}