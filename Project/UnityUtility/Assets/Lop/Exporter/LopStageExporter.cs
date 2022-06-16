// System
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
using LitJson;

public class LopStageExporter : MonoBehaviour
{
    private void Start()
    {
        List<LopStageFormat> lopStageFormatList = ExportStageDataAsync();
        );
    }

    private async Task<List<LopStageFormat>> ExportStageDataAsync()
    {
        List<LopStageFormat> lopStageFormatList = new List<LopStageFormat>();
        SpriteRenderer[] rendererArray = transform.GetComponentsInChildren<SpriteRenderer>(true);

        Debug.Log($"Count : {rendererArray.Length}");

        int distance = 128;
        Parallel.For(0, rendererArray.Length, (i) =>
        {
            string name = rendererArray[i].sprite.name;
            Vector3 pos = rendererArray[i].transform.position;
            Vector3 _pos = rendererArray[i].transform.position;

            float x = _pos.x;
            float y = _pos.y;
            float z = _pos.z;

            if (!name.ToUpper().Contains("FISH"))
            {
                x = Mathf.Round(x);
                y = Mathf.Round(y);
                z = Mathf.Round(z);
            }
        });

        await File.WriteAllText(Path.Combine(Application.persistentDataPath, "rendererArray.txt"), result);


        return null;
    }
}