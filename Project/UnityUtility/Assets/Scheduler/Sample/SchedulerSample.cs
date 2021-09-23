// System
using System.Collections;

// Unity
using UnityEngine;

public class SchedulerSample : MonoBehaviour
{
    private void Start()
    {
        Schedule schedule1 = new Schedule("name_00", 0, 0, Co_Test("123"));
        Schedule schedule2 = new Schedule("name_01", 0, 0, Co_Test("456"));

        Scheduler.Add(schedule1);
        Scheduler.Add(schedule2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddTestCase();
        }
    }

    public int index = 3;
    public void AddTestCase()
    {
        Scheduler.Add(new Schedule($"name_{index:D2}", 0, 0, Co_Test($"{index:D2}")));
        index++;
    }

    private IEnumerator Co_Test(string log)
    {
        Debug.Log($"start : {log}");
        yield return new WaitForSeconds(3.0f);

    }
}