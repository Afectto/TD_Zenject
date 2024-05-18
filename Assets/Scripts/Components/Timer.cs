using UnityEngine;

public class Timer
{
    private float startTime;
    private float currentTime;

    public Timer()
    {
        Reset();
    }

    public void Start()
    {
        startTime = Time.time;
    }

    public void Update()
    {
        currentTime = Time.time - startTime;
    }

    public void Reset()
    {
        startTime = Time.time;
        currentTime = 0f;
    }
    
    public int GetMinutes()
    {
        return Mathf.FloorToInt(currentTime / 60);
    }

    public int GetSeconds()
    {
        return Mathf.FloorToInt(currentTime % 60);
    }

    public string GetTimeFormatted()
    {
        return string.Format("{0:D2}:{1:D2}", GetMinutes(), GetSeconds());
    }
}