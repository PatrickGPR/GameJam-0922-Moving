using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class UITimerDisplayer : MonoBehaviour
{
    private TMP_Text timerText;

    private void Awake()
    {
        timerText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        timerText.text = ToTimerFormat(GameManager.Instance.TimeLeft);
    }

    private void Update()
    {
        timerText.text = ToTimerFormat(GameManager.Instance.TimeLeft);
    }

    private string ToTimerFormat(float time)
    {
        var minutes = (int)time / 60;
        var seconds = (int)(time % 60);

        string minuteString = minutes < 10 ? $"0{minutes}" : minutes.ToString(CultureInfo.InvariantCulture);
        string secondsString = seconds < 10 ? $"0{seconds}" : seconds.ToString(CultureInfo.InvariantCulture);

        return $"{minuteString}:{secondsString}";
    }
}
