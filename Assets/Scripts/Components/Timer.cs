using System.Collections;
using UnityEngine;

public class Timer
{
    private float startTime;
    private float currentTime;
    
    private Utils.Coroutines Coroutines;

    public Timer()
    {
        Reset();
        Coroutines = new GameObject("[COROUTINES]").AddComponent<Utils.Coroutines>();
    }

    public void Start()
    {
        startTime = Time.time;
        Coroutines.StartCoroutine(Update());
    }

    private IEnumerator Update()
    {
        while (true)
        {
            currentTime = Time.time - startTime;
            yield return null;
        }
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