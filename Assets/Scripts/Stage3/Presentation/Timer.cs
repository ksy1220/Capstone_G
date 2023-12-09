using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI minutesText;
    public TextMeshProUGUI secondsText;
    public TextMeshProUGUI millisecondsText;

    private float countdownTime = 60.0f; // 분을 초로 표현

    private void Update()
    {
        countdownTime -= Time.deltaTime;

        if (countdownTime <= 0)
        {
            countdownTime = 0;
            // 타이머 만료 시 처리 (게임 종료, 메시지 표시 등)를 여기에 작성합니다.
        }

        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        int milliseconds = Mathf.FloorToInt((countdownTime * 1000) % 100);

        minutesText.text = minutes.ToString("00");
        secondsText.text = seconds.ToString("00");
        millisecondsText.text = milliseconds.ToString("00");
    }
}