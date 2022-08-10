// System
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

// Unity
using UnityEngine.Networking;

public static class UnityWebRequestorExtension
{
    public static TaskAwaiter<UnityWebRequest.Result> GetAwaiter(this UnityWebRequestAsyncOperation operation)
    {
        TaskCompletionSource<UnityWebRequest.Result> source = new TaskCompletionSource<UnityWebRequest.Result>();
        operation.completed += asyncOperation => source.TrySetResult(operation.webRequest.result);

        if (operation.isDone)
            source.TrySetResult(operation.webRequest.result);

        return source.Task.GetAwaiter();
    }
}