using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public static Clock Instance { get; private set; }

    [SerializeField] Transform indicatorPivotPrefab;
    [SerializeField] Transform hourHand;
    [SerializeField] Transform minuteHand;
    [SerializeField] Transform secondHand;
    bool automaticWatches = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnIndicators();

        Debug.Log("Time: " + DateTime.Now);

        hourHand.localRotation = Quaternion.Euler(0, 0, DateTime.Now.Hour * -30);    // 360/12 = 30
        minuteHand.localRotation = Quaternion.Euler(0, 0, DateTime.Now.Minute * -6); // 360/60 = 6
        secondHand.localRotation = Quaternion.Euler(0, 0, DateTime.Now.Second * -6); // 360/60 = 6
    }

    void Update()
    {
        if (automaticWatches)
        {
            AutomaticClock();
        }
        else
        {
            AnalogClock();
        }
    }

    void SpawnIndicators()
    {
        for (int i = 0; i < 12; i++)
        {
            Transform indicatorPivot = Instantiate(indicatorPivotPrefab, transform);
            indicatorPivot.localRotation = Quaternion.Euler(0, 0, i * 30);
            Transform indicator = indicatorPivot.GetChild(0);

            if (i % 3 == 0) indicator.localScale *= 1.5f;
        }
    }

    void AnalogClock()
    {
        var time = DateTime.Now;
        hourHand.localRotation = Quaternion.Euler(0, 0, time.Hour * -30 + time.Minute * -0.5f); // 360/12 = 30, 30/60 = 0.5
        minuteHand.localRotation = Quaternion.Euler(0, 0, time.Minute * -6 + time.Second * -0.1f); // 360/60 = 6, 6/60 = 0.1
        secondHand.localRotation = Quaternion.Euler(0, 0, time.Second * -6); // 360/60 = 6
    }

    void AutomaticClock()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;
        hourHand.localRotation = Quaternion.Euler(0, 0, (float)time.TotalHours * -30);
        minuteHand.localRotation = Quaternion.Euler(0, 0, (float)time.TotalMinutes * -6);
        secondHand.localRotation = Quaternion.Euler(0, 0, (float)time.TotalSeconds * -6);
    }

    public string ToggleAutomaticWatches()
    {
        automaticWatches = !automaticWatches;

        return automaticWatches ? "Automatic" : "Analog";
    }
}
