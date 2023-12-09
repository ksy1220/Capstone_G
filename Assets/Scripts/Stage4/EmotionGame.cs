using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class EmotionGame : MonoBehaviour
{
    [SerializeField]
    GameObject face1, face2, face3, face4, face5, face6, face0;
    public Slider timerSlider;
    public Button[] SelectButtons;
    public int totalQuestions = 6;
    public float questionTimeLimit = 10.0f;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI countText;

    private int[] answer = { 0, 2, 7, 7, 2, 1 };
    private int currentQuestion = 0; // 초기 이미지 인덱스
    private int correctAnswers = 0; // 맞춘 문제 수
    private bool isGameActive = true;
    private float timeRemaining;

    void Start()
    {
        // 초기화 코드 추가
        timeRemaining = questionTimeLimit;
        UpdateTimerSlider();

        // 처음 이미지 표시
        ShowImage(currentQuestion);

        // count 텍스트 초기화
        UpdateCountText();
    }

    void Update()
    {
        if (isGameActive)
        {
            // 시간 업데이트 및 체크
            timeRemaining -= Time.deltaTime;
            UpdateTimerSlider();

            if (timeRemaining <= 0)
            {
                // 시간 종료 시 처리
                Debug.Log("시간초과");
                StartCoroutine(DisplayResult("시간 초과", 1.0f)); // 시간 초과 메시지
                timeRemaining = questionTimeLimit;
            }
        }
    }

    void UpdateTimerSlider()
    {
        // 슬라이더의 minValue와 maxValue 설정
        timerSlider.minValue = 0f;
        timerSlider.maxValue = questionTimeLimit;

        // 현재 시간에 따라 슬라이더의 value 업데이트
        timerSlider.value = questionTimeLimit - timeRemaining;
    }

    void ShowImage(int imageIndex)
    {
        if (imageIndex >= 0 && imageIndex < 6)
        {
            switch (imageIndex)
            {
                case 0:
                    face1.SetActive(true);
                    face2.SetActive(false);
                    face3.SetActive(false);
                    face4.SetActive(false);
                    face5.SetActive(false);
                    face6.SetActive(false);
                    face0.SetActive(false);
                    break;
                case 1:
                    face1.SetActive(false);
                    face2.SetActive(true);
                    face3.SetActive(false);
                    face4.SetActive(false);
                    face5.SetActive(false);
                    face6.SetActive(false);
                    face0.SetActive(false);
                    break;
                case 2:
                    face1.SetActive(false);
                    face2.SetActive(false);
                    face3.SetActive(true);
                    face4.SetActive(false);
                    face5.SetActive(false);
                    face6.SetActive(false);
                    face0.SetActive(false);
                    break;
                case 3:
                    face1.SetActive(false);
                    face2.SetActive(false);
                    face3.SetActive(false);
                    face4.SetActive(true);
                    face5.SetActive(false);
                    face6.SetActive(false);
                    face0.SetActive(false);
                    break;
                case 4:
                    face1.SetActive(false);
                    face2.SetActive(false);
                    face3.SetActive(false);
                    face4.SetActive(false);
                    face5.SetActive(true);
                    face6.SetActive(false);
                    face0.SetActive(false);
                    break;
                case 5:
                    face1.SetActive(false);
                    face2.SetActive(false);
                    face3.SetActive(false);
                    face4.SetActive(false);
                    face5.SetActive(false);
                    face6.SetActive(true);
                    face0.SetActive(false);
                    break;
                default:
                    face1.SetActive(false);
                    face2.SetActive(false);
                    face3.SetActive(false);
                    face4.SetActive(false);
                    face5.SetActive(false);
                    face6.SetActive(false);
                    face0.SetActive(true);
                    break;
            }
        }
    }
    private IEnumerator DisplayResult(string message, float delay)
    {
        resultText.text = message;
        isGameActive = false;
        yield return new WaitForSeconds(delay);
        isGameActive = true;
        resultText.text = "";
        NextQuestion();
    }

    bool IsCorrectAnswer(int buttonIndex)
    {
        // 현재 이미지에 해당하는 정답 버튼 확인 로직
        return answer[currentQuestion] == buttonIndex;
    }


    void NextQuestion()
    {
        if (currentQuestion < totalQuestions - 1)
        {
            // 다음 문제로 진행
            currentQuestion++;
            timeRemaining = questionTimeLimit;
            UpdateTimerSlider();
            ShowImage(currentQuestion);
            resultText.text = ""; // 결과 메시지 초기화
        }
        else
        {
            // 게임 종료 처리
            StartCoroutine(DisplayResultAndMoveScene("게임종료", 3.0f));
            isGameActive = false;
            Debug.Log("게임 종료");
        }
    }
    IEnumerator DisplayResultAndMoveScene(string message, float delay)
    {
        resultText.text = message;

        yield return new WaitForSeconds(delay);

        // 3초 후에 씬 이동
        SceneManager.LoadScene("stage4_interview");
    }
    void UpdateCountText()
    {
        // count 텍스트 업데이트
        countText.text = "문제: " + (currentQuestion + 1) + " / 맞힌 문제: " + correctAnswers;
    }

    public void ButtonClicked(int buttonIndex)
    {
        if (isGameActive)
        {
            SoundManager.instance.PlaySFX(Sfx.button_ui);

            if (IsCorrectAnswer(buttonIndex))
            {
                StartCoroutine(DisplayResult("정답!", 1.0f));
                correctAnswers++;
            }
            else
            {
                StartCoroutine(DisplayResult("오답!", 1.0f));
            }
            UpdateCountText(); // 맞춘 문제 수를 업데이트
        }
    }
}
