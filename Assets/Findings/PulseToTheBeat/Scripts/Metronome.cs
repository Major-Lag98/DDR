using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Metronome : MonoBehaviour
{
    public float bpm = 120.0F;
    public AudioSource audioSource;
    public Intervals[] intervals;

    private void Start()
    {
        Application.runInBackground = true;
    }

    private void Update()
    {
        foreach (Intervals interval in intervals)
        {
            float sampledTime = audioSource.timeSamples / ((float)audioSource.clip.frequency * interval.GetIntervalLength(bpm));
            interval.CheckForNewInterval(sampledTime);
        }
    }

}

[System.Serializable]
public class  Intervals
{
    [SerializeField] public float steps;
    [SerializeField] UnityEvent trigger;
    public int lastInterval;

    public float GetIntervalLength(float bpm)
    {
        return 60f / (steps * bpm);
    }

    public void CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != lastInterval)
        {
            lastInterval = Mathf.FloorToInt(interval);
            trigger.Invoke();
        }
    }
}
