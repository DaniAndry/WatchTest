using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElectronicAlarmClock : AlarmClock
{
    [SerializeField] private GameObject _alarmClockPanel;
    [SerializeField] private TMP_InputField hoursInput;
    [SerializeField] private TMP_InputField minutesInput;
    [SerializeField] private Button _set;
    [SerializeField] private PanelClickEvent _panelClickEvent;

    private void Start()
    {
        _alarmClockPanel.SetActive(false);

        _panelClickEvent.OnPanelClick.AddListener(OnPanel);
    }
    public void ClosePanel()
    {
        _alarmClockPanel.SetActive(false);
    }

    private void OnPanel()
    {
        _alarmClockPanel.SetActive(true);

    }

    public void Input()
    {
        int hours;
        int minutes;

        if (int.TryParse(hoursInput.text, out hours) && int.TryParse(minutesInput.text, out minutes))
        {
            SetAlarm(hours, minutes);
            _alarmClockPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Не корректное время!");
        }
    }

}


