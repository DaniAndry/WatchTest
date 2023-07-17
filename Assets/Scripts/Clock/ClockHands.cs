using System;
using UnityEngine;

public class ClockHands : MonoBehaviour
{
    [SerializeField] private Clock _clock;

    [SerializeField] private Transform _hourHand;
    [SerializeField] private Transform _minuteHand;
    [SerializeField] private Transform _secondHand;

    private bool _shouldUpdateHands = true;

    private float _hours;
    private float _minutes;
    private float _seconds;

    private void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        if (_shouldUpdateHands)
        {
            RotateClockHands();
            _hours = _clock.Time.Hour;
            _minutes = _clock.Time.Minute;
            _seconds = _clock.Time.Second;
        }
        else
        {
            InverseRotateClockHands();
        }
    }

    private void InverseRotateClockHands()
    {
        Vector3 hourRotation = _hourHand.localEulerAngles;
        Vector3 minuteRotation = _minuteHand.localEulerAngles;

        _hours = (hourRotation.z / -30f); 
        _minutes = minuteRotation.z / -6f; 

        _hours = Mathf.Repeat(_hours, 24f);
        _minutes = Mathf.Repeat(_minutes, 60f);


        Debug.Log("Часы: " + _hours + ", Минуты: " + _minutes);
    }



    private void RotateClockHands()
    {
        float hourRotation = 360f * (_hours % 12f) / 12f;
        float minuteRotation = 360f * _minutes / 60f;
        float secondRotation = 360f * _seconds / 60f;

        _hourHand.localRotation = Quaternion.Euler(0f, 0f, -hourRotation);
        _minuteHand.localRotation = Quaternion.Euler(0f, 0f, -minuteRotation);
        _secondHand.localRotation = Quaternion.Euler(0f, 0f, -secondRotation);
    }

    public void ToggleAutomaticUpdate(bool shouldUpdate)
    {
        _shouldUpdateHands = shouldUpdate;
    }

    public DateTime GetTime()
    {
        return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, (int)_hours, (int)_minutes, 0);
    }
}
