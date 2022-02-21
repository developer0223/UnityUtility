// System
using System;
using System.Threading;
using System.Threading.Tasks;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class ThreadHelperSample : MonoBehaviour
{
    public Text txt_log = null;

    private Task sampleTask = null;
    private CancellationTokenSource sampleTaskCancelTokenSource = new CancellationTokenSource();
    private CancellationToken sampleTaskCancelToken;

    private static System.Object lockObject = new System.Object();

    private void Start()
    {
        txt_log.text = string.Empty;

        sampleTaskCancelTokenSource = new CancellationTokenSource();
        sampleTaskCancelToken = sampleTaskCancelTokenSource.Token;

        Run();
    }

    private async void Run()
    {
        // Must be created before run task
        MainThreadHelper.GetOrCreate();

        sampleTask = Task.Factory.StartNew(SampleTask, sampleTaskCancelToken);
        await sampleTask;
    }

    private async void SampleTask()
    {
        Debug.Log($"SampleTask Started");

        //sampleTaskCancelToken.ThrowIfCancellationRequested();

        int index = 0;
        while (true)
        {
            if (sampleTaskCancelToken.IsCancellationRequested)
                return;

            lock (lockObject)
            {
                MainThreadHelper.AddAction(() =>
                {
                    txt_log.text += $"{index}\t";
                    Debug.Log($"{index}\t");
                    index++;
                });
            }


            await Task.Delay(1000);
        }
    }

    private void OnDestroy()
    {
        sampleTaskCancelTokenSource.Cancel();
    }
}