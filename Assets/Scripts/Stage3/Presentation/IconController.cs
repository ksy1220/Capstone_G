using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour
{
    public GameObject bubble;
    public GameObject eyeIcon;
    public GameObject heartIcon;

    private bool isEyeVisible = false;
    private bool isHeartVisible = false;
    private float heartVisibleTime = 0f; // 하트 아이콘 표시 시간을 추적하기 위한 변수
    private float timeElapsed = 0f;
    private float minInterval = 10f; // 최소 시간 간격 (초)
    private float maxInterval = 20f; // 최대 시간 간격 (초)
    private float nextEyeIconTime;

    private void Start()
    {
        // 초기 상태에서는 모든 아이콘을 비활성화합니다.
        eyeIcon.SetActive(false);
        heartIcon.SetActive(false);
        bubble.SetActive(false);

        // 최초 눈 아이콘을 나타내기 위해 다음 시간을 무작위로 설정합니다.
        SetNextEyeIconTime();
    }

    private void Update()
{
    timeElapsed += Time.deltaTime;

    if (timeElapsed < 100f)
    {
        if (timeElapsed >= nextEyeIconTime)
        {
            ShowEyeIcon();
            SetNextEyeIconTime();
        }
    }

    if (isHeartVisible)
    {
        heartVisibleTime += Time.deltaTime; // 하트 아이콘 표시 시간 업데이트

        if (heartVisibleTime >= 0.5f)
        {
            HideHeartAndBubble();
            heartVisibleTime = 0f; // 하트 아이콘 표시 시간 리셋

            if (timeElapsed < 100f)
            {
                ShowRandomEyeIcon();
            }
        }
    }
}

    private void SetNextEyeIconTime()
{
    // 현재 timeElapsed에 무작위 시간을 추가하여 다음 눈 아이콘 시간을 설정합니다.
    nextEyeIconTime = timeElapsed + Random.Range(minInterval, maxInterval);
}

    private void ShowEyeIcon()
    {
        // 눈 아이콘을 표시하고 말풍선도 표시합니다.
        eyeIcon.SetActive(true);
        bubble.SetActive(true);
        isEyeVisible = true;
    }

    private void HideHeartAndBubble()
    {
        // 하트 아이콘과 말풍선을 숨깁니다.
        heartIcon.SetActive(false);
        bubble.SetActive(false);
        isHeartVisible = false;
    }

    private void ShowRandomEyeIcon()
{
    if (timeElapsed < 100f) // 100초가 지나지 않았다면
    {
        // 다른 학생에게 랜덤하게 눈 아이콘을 나타냅니다.
        int randomStudent = Random.Range(1, 7);
        if (gameObject.name == "student_" + randomStudent)
        {
            ShowEyeIcon();
        }
    }
}

    private void OnMouseDown()
    {
        if (isEyeVisible && !isHeartVisible)
    {
        // 눈 아이콘을 숨깁니다.
        eyeIcon.SetActive(false);

        // 하트 아이콘을 보여줍니다.
        heartIcon.SetActive(true);

        // 하트 아이콘의 표시 상태를 true로 설정합니다.
        isHeartVisible = true;

        // 시간 계산을 위해 타이머를 리셋합니다.
        heartVisibleTime = 0f;

        // Debugging line to check heart icon activity
        Debug.Log("Heart Icon Active: " + heartIcon.activeSelf);

        // Rest of your code...
}
}
}