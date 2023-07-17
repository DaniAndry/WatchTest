using System;
using UnityEngine;

public class AnalogAlarmClock : AlarmClock
{
    [SerializeField] private ClockHands _clockHands;
    [SerializeField] private PanelClickEvent _panelClickEvent;
    [SerializeField] private GameObject _setButton;

    private int _hours;
    private int _minutes;


    private void Start()
    {
        _panelClickEvent.OnPanelClick.AddListener(SetIsWork);
        _clockHands.ToggleAutomaticUpdate(true);
    }

    private void SetIsWork()
    {
        _setButton.SetActive(true);
        _clockHands.ToggleAutomaticUpdate(false);
    }


    public void SetAnalogAlarm()
    {

        SetAlarm(_clockHands.GetTime().Hour, _clockHands.GetTime().Minute);
        _setButton.SetActive(false);
        _clockHands.ToggleAutomaticUpdate(true);
    }
}