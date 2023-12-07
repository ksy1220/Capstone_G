using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class RockPaperScissorsGame : MonoBehaviour
{
    public TextMeshProUGUI resultText, round;
    public GameObject image1, image2, image3, image4, image5, image6;
    public GameObject rps_me, rps_computer;
    public Button rock, scissor, paper;
    public Slider timerSlider;
    public float questionTimeLimit = 3.0f;
    private string[] correctAnswers = { "Paper", "Scissor", "Rock", "Rock", "Paper", "Scissor" };
    private int number;
    private int currentGame = 0;
    private float timeRemaining;
    private bool isGameActive = true;
    private int score = 0; // 점수 변수 추가

    void Start()
    {
        StartCoroutine(PlayGame());
    }
    IEnumerator PlayGame()
    {
        while (currentGame < 10) // Run 10 games
        {
            initial();
            int roundtext = currentGame + 1;
            round.text = "< " + roundtext + " 라운드 >";
            number = Random.Range(1, 7);
            ShowRandomScenario(number);
            timeRemaining = questionTimeLimit;
            isGameActive = true;

            while (timeRemaining > 0 && isGameActive)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerSlider();
                yield return null;
            }
            yield return new WaitForSeconds(1.0f); // Delay between rounds
        }

        // Game Over
        Debug.Log("Game Over!");
    }
    void UpdateTimerSlider()
    {
        // 슬라이더의 minValue와 maxValue 설정
        timerSlider.minValue = 0f;
        timerSlider.maxValue = questionTimeLimit;

        // 현재 시간에 따라 슬라이더의 value 업데이트
        timerSlider.value = questionTimeLimit - timeRemaining;
    }

    public void ShowRandomScenario(int number)
    {
        switch (number)
        {
            case 1:
                rps_me.SetActive(false);
                image1.SetActive(true);
                break;
            case 2:
                rps_me.SetActive(false);
                image2.SetActive(true);
                break;
            case 3:
                rps_me.SetActive(false);
                image3.SetActive(true);
                break;
            case 4:
                rps_computer.SetActive(false);
                image4.SetActive(true);
                break;
            case 5:
                rps_computer.SetActive(false);
                image5.SetActive(true);
                break;
            case 6:
                rps_computer.SetActive(false);
                image6.SetActive(true);
                break;

        }
    }
    public void initial()
    {
        resultText.color = Color.black;
        resultText.text = "가위 바위 보!";
        rps_me.SetActive(true);
        rps_computer.SetActive(true);
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        image4.SetActive(false);
        image5.SetActive(false);
        image6.SetActive(false);
    }
    public void CheckAnswer(string userChoice)
    {
        // 현재 시나리오에 대한 정답 가져오기
        int currentScenarioIndex = number;
        string correctAnswer = correctAnswers[currentScenarioIndex - 1];

        // 사용자 선택과 정답 비교
        if (userChoice.Equals(correctAnswer))
        {
            resultText.color = Color.blue;
            resultText.text = "성공입니다!";
            score++; // 정답을 맞춘 경우 점수 증가
            Debug.Log("Correct Answer!");
            // 여기에 정답일 때 실행할 동작 추가
        }
        else
        {
            resultText.color = Color.red;
            resultText.text = "실패입니다!";
            Debug.Log("Incorrect Answer!");
            // 여기에 오답일 때 실행할 동작 추가
        }
        currentGame++;
        // 게임이 끝났을 때 점수 표시
        if (currentGame == 10)
        {
            ShowScore();
        }
    }
    private void ShowScore()
    {
        int finalScore = score * 10; // 성공 횟수 * 10으로 최종 점수 계산
        resultText.color = Color.green;
        resultText.text = "총 점수 : " + finalScore + " / 100";

        StartCoroutine(ShowScoreAndProceed());
    }

    private IEnumerator ShowScoreAndProceed()
    {
        yield return new WaitForSeconds(3.0f); // 5초 동안 대기

        resultText.color = Color.black;
        resultText.text = "AI 게임이 종료되었습니다.";

        yield return new WaitForSeconds(2.0f); // 원하는 대기 시간 설정

        SceneManager.LoadScene("stage4_interview");
    }

    public void ChangeImage(string userChoice)
    {
        if (userChoice == "Rock")
        {
            if (number < 4)
            {
                rps_computer.SetActive(false);
                image5.SetActive(true);
            }
            else
            {
                rps_me.SetActive(false);
                image2.SetActive(true);
            }
        }
        if (userChoice == "Scissor")
        {
            if (number < 4)
            {
                rps_computer.SetActive(false);
                image4.SetActive(true);
            }
            else
            {
                rps_me.SetActive(false);
                image1.SetActive(true);
            }
        }
        if (userChoice == "Paper")
        {
            if (number < 4)
            {
                rps_computer.SetActive(false);
                image6.SetActive(true);
            }
            else
            {
                rps_me.SetActive(false);
                image3.SetActive(true);
            }
        }
    }
    public void OnRockButtonClicked()
    {
        if (!isGameActive) return;
        isGameActive = false;
        ChangeImage("Rock");
        CheckAnswer("Rock");
    }

    public void OnScissorButtonClicked()
    {
        if (!isGameActive) return;
        isGameActive = false;
        ChangeImage("Scissor");
        CheckAnswer("Scissor");

    }

    public void OnPaperButtonClicked()
    {
        if (!isGameActive) return;
        isGameActive = false;
        ChangeImage("Paper");
        CheckAnswer("Paper");
    }

}
