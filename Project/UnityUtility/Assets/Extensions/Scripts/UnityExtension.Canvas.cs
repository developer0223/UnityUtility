// Unity
using UnityEngine;

/// <summary>
/// Extension class for UnityEngine.Canvas
/// </summary>
public static partial class UnityExtension 
{
    public static void SetOnTop(this Canvas canvas)
    {
        Canvas[] canvasArray = GameObject.FindObjectsOfType<Canvas>(true);
        int biggest = 0;
        for (int i = 0; i < canvasArray.Length; i++)
        {
            if (canvasArray[i] == canvas)
                continue;

            int current = canvasArray[i].sortingOrder;
            if (current > biggest)
                biggest = current;
        }

        canvas.sortingOrder = biggest + 1;
    }
}