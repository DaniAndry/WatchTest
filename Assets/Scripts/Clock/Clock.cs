using System;
using UnityEngine;
using System.Net.Sockets;
using System.Net;

public class Clock : MonoBehaviour
{
    const string ntpServer1 = "ntp0.ntp-servers.net";
    const string ntpServer2 = "ntp3.ntp-servers.net";

    public DateTime Time { get; private set; }

    private DateTime _ntpTime;
    private DateTime _ntpTime1;
    private DateTime _ntpTime2;
    private DateTime _currentLocalTime;
    private TimeSpan _timeDifference;


    private void Awake()
    {
        _currentLocalTime = DateTime.Now;
        GetNtpTime();
        InvokeRepeating(nameof(GetNtpTime), 3600f, 3600f);
        UpdateTime();
    }

    private void GetNtpTime()
    {
        _ntpTime1 = GetTimeFromNtpServer(ntpServer1);
        _ntpTime2 = GetTimeFromNtpServer(ntpServer2);

        UpdateTime();
    }


    private void Update()
    {
        _currentLocalTime = DateTime.Now;
        Time = _currentLocalTime - _timeDifference;
    }

    private void UpdateTime()
    {
        if (_ntpTime1 != default)
        {
            _ntpTime = _ntpTime1;
        }
        else if (_ntpTime2 != default)
        {
            _ntpTime = _ntpTime2;
        }
        else
        {
            _ntpTime = _currentLocalTime;
        }

        if (_currentLocalTime > _ntpTime)
        {
            _timeDifference = _currentLocalTime - _ntpTime;

        }
        else
        {
            _timeDifference = _ntpTime - _currentLocalTime;
        }
    }

    private DateTime GetTimeFromNtpServer(string ntpServer)
    {
        var ntpData = new byte[48];
        ntpData[0] = 0x1B;

        var addresses = Dns.GetHostEntry(ntpServer).AddressList;
        var ipEndPoint = new IPEndPoint(addresses[0], 123);
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        socket.Connect(ipEndPoint);
        socket.Send(ntpData);
        socket.Receive(ntpData);
        socket.Close();

        ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
        ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

        var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
        var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);

        return networkDateTime;
    }
}

