using System;
using UnityEngine;

public class AlarmClock : MonoBehaviour
{
    [SerializeField] private Clock _clock;

    protected static bool IsAlarmSet;
    protected DateTime _alarmTime;

    protected void SetAlarm(int hours, int minutes)
    {
        if (!IsAlarmSet)
        {
            _alarmTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, 0);
            Debug.Log("Будильник установлен на " + _alarmTime);
            IsAlarmSet = true;
        }
        else if (IsAlarmSet)
        {
            Debug.Log("Будильник уже установлен!");
        }
    }

    private void LateUpdate()
    {
        if (IsAlarmSet)
        {
            DateTime currentTime = _clock.Time;

            TimeSpan difference = currentTime - _alarmTime;

            if (Math.Abs(difference.TotalSeconds) <= 1)
            {
                Debug.Log("Дилинь, дилинь!");
                IsAlarmSet = false;
            }
        }
    }
}
