using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpdateTimer : MonoBehaviour
{
    [Inject] private EventManager _eventManager;
    
    private Timer _time;
    [SerializeField]private Text _timeText;
    
    private void Awake()
    {
        _time = new Timer();
        _time.Start();
    }

    private void Update()
    {
        var timeValue = 30 - _time.GetSeconds();
        _time.Update();
        if (timeValue < 0)
        {
            timeValue = 30;
            _time.Reset();
            _eventManager?.TriggerOnNeedUpdateShop();
        }
        
        _timeText.text = timeValue.ToString() + " sec.";
    }
}
