// System
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class Scheduler : MonoBehaviour
{
    public static readonly string Path = "Prefabs/UnityUtility/Scheduler";

    public List<Schedule> list = new List<Schedule>();

    public static Scheduler GetOrCreate()
    {
        Scheduler _script = FindObjectOfType<Scheduler>();
        if (!_script)
        {
            GameObject _prefab = Resources.Load(Path) as GameObject;
            GameObject _gameObject = Instantiate(_prefab);
            _script = _gameObject.GetComponent<Scheduler>();
        }

        return _script;
    }

    public static void Add(Schedule schedule)
    {
        Scheduler scheduler = GetOrCreate();
        if (!Contains(schedule.name, out _))
            scheduler.list.Add(schedule);
        else
            Debug.LogWarning("Cannot add schedule. (Same name already exists)");
    }

    public static bool Remove(string name)
    {
        Scheduler scheduler = GetOrCreate();
        if (Contains(name, out int index))
        {
            if (index == 0)
            {
                Debug.LogWarning("Cannot remove schedule. (Process already started)");
            }
            else
            {
                scheduler.list.RemoveAt(index);
                return true;
            }
        }
        return false;
    }

    private static bool Contains(string name, out int index)
    {
        Scheduler scheduler = GetOrCreate();
        for (int i = 0; i < scheduler.list.Count; i++)
        {
            if (scheduler.list[i].name == name)
            {
                index = i;
                return true;
            }
        }

        index = -1;
        return false;
    }

    private void Start()
    {
        StartCoroutine(Co_ExecuteWhenExists());
    }

    private IEnumerator Co_ExecuteWhenExists()
    {
        while (true)
        {
            if (list.Count >= 1)
            {
                yield return StartCoroutine(list[0].callback);
                list.RemoveAt(0);
            }
            else
            {
                yield return null;
            }
        }
    }
}