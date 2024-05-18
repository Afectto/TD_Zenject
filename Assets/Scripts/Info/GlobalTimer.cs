using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalTimer : MonoBehaviour
{
    private Timer _time;
    [SerializeField]private Text _timeText;
    
    private void Awake()
    {
        _time = new Timer();
        _time.Start();
    }

    private void Update()
    {
        _time.Update();
        _timeText.text = _time.GetTimeFormatted();
    }
}
