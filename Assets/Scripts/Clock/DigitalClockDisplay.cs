using UnityEngine;
using TMPro;
using System;

public class DigitalClockDisplay : MonoBehaviour
{
    [SerializeField] private Clock _clockNet;

    [SerializeField] private TextMeshProUGUI _hoursText;
    [SerializeField] private TextMeshProUGUI _minutesText;
    [SerializeField] private TextMeshProUGUI _secondsText;

    private void Update()
    {
        DateTime now = _clockNet.Time;
        string hoursString = $"{now.Hour}";
        string minutesString = $"{now.Minute}";
        string secondsString = $"{now.Second}";

        _hoursText.text = hoursString;
        _minutesText.text = minutesString;
        _secondsText.text = secondsString;
    }
}
