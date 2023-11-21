using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    public TextMeshProUGUI timeText; // TMP Text UI를 참조합니다.
    private float elapsedTime = 0f;

    private void Start()
    {
        // 시작 시간을 "00:00:00"으로 설정
        timeText.text = "00:00:00";
    }

    private void Update()
    {
        // 경과 시간 업데이트
        elapsedTime += Time.deltaTime;

        // 경과 시간을 HH:mm:ss 형식의 문자열로 변환
        string formattedTime = FormatTime(elapsedTime);

        // TMP Text 업데이트
        timeText.text = formattedTime;
    }

    // 초를 HH:mm:ss 형식의 문자열로 변환하는 함수
    private string FormatTime(float timeInSeconds)
    {
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}

