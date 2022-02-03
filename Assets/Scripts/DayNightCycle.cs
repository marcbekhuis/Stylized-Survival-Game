using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    public GameObject light;
    public int durationOfHour;
    public float durationOfDay;
    [Range(0, 24)] public float timeOfDay = 12;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var rotation = new Vector3(360 / 24 * timeOfDay, 0, 0);

        light.transform.eulerAngles = rotation;
        SetTime();
    }

    private void SetTime()
    {
        timeOfDay += 24f / durationOfDay / 60 * Time.deltaTime;
        if (timeOfDay > 24) timeOfDay -= 24;
    }
}
