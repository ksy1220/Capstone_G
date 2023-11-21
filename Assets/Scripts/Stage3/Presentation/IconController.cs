using UnityEngine;

public class IconController : MonoBehaviour
{
    public GameObject bubble;
    public GameObject eyeIcon;
    public GameObject heartIcon;

    private bool isEyeVisible = false;
    private bool isHeartVisible = false;
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

        if (timeElapsed >= nextEyeIconTime)
        {
            // 설정된 다음 눈 아이콘 시간에 눈 아이콘을 나타냅니다.
            ShowEyeIcon();
            SetNextEyeIconTime(); // 다음 눈 아이콘 시간을 설정합니다.
        }

        if (isHeartVisible)
        {
            // 하트 아이콘이 보이고 있는 경우, 0.3초 뒤에 숨깁니다.
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= 0.3f)
            {
                HideHeartAndBubble();

                // 랜덤하게 다른 학생에게 눈 아이콘을 나타냅니다.
                ShowRandomEyeIcon();
            }
        }
    }

    private void SetNextEyeIconTime()
    {
        // 최소 시간 간격과 최대 시간 간격 사이의 무작위 시간을 선택합니다.
        nextEyeIconTime = Random.Range(minInterval, maxInterval);
        timeElapsed = 0f; // 타이머를 리셋합니다.
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
        // 다른 학생에게 랜덤하게 눈 아이콘을 나타냅니다.
        int randomStudent = Random.Range(1, 7);
        if (gameObject.name == "student_" + randomStudent)
        {
            ShowEyeIcon();
        }
    }

    private void OnMouseDown()
    {
        if (isEyeVisible && !isHeartVisible)
        {
            // 눈 아이콘을 클릭하면 호출됩니다.
            // 눈 아이콘을 숨기고 하트 아이콘을 보이게 변경
            eyeIcon.SetActive(false);
            heartIcon.SetActive(true);

            // 0.3초 후에 하트 아이콘을 숨깁니다.
            isHeartVisible = true;
            timeElapsed = 0f; // 하트 아이콘의 표시 시간을 측정하기 위해 타이머를 리셋합니다.
        }
    }
}