using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PresentationManager : MonoBehaviour
{
    public Image slideImage;
    public Button[] optionButtons; // 2개의 버튼만 사용
    public Sprite[] slideSprites;

    private int currentSlideIndex = 0;
    private int totalScore = 0; // 총 점수를 저장하는 변수

    public SlidesManager slidesManager;
    public Button myButton;
    public GameObject timer;
    public GameObject ui;
    public GameObject students;
    public GameObject resultPanel; // 결과를 표시할 패널
    public Sprite clearImg; // 승리 이미지
    public Sprite failImg; // 실패 이미지

    private bool gameIntroShown = false;

    // 버튼 점수 배열 추가
    private int[,] buttonScores; // 각 슬라이드에 대한 각 버튼의 점수

    public void SetSlideGroup(int selectedIndex)
    {
        int groupSize = 8; // 각 그룹의 슬라이드 크기
        currentSlideIndex = selectedIndex * groupSize; // 시작 인덱스 설정
    }

    public void StartPresentationGame()
    {
        // 초기에는 gameIntro만 활성화
        timer.SetActive(false);
        ui.SetActive(false);
        students.SetActive(false);

        // 버튼 클릭 이벤트 리스너 추가
        myButton.onClick.AddListener(OnButtonClick);

        // 각 버튼에 클릭 이벤트 리스너 추가
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int buttonIndex = i;
            optionButtons[i].onClick.AddListener(() => OnOptionButtonClicked(buttonIndex));
        }

        // PlayerPrefs에서 선택된 슬라이드 인덱스 검색
        if (PlayerPrefs.HasKey("SelectedSlideIndex"))
        {
            int selectedIndex = PlayerPrefs.GetInt("SelectedSlideIndex");

            SetSlideGroup(selectedIndex);
            InitializeButtonScores(); // 버튼 점수 초기화
            InitializeButtonContents();

            Debug.Log("Starting SlideShowCoroutine"); // 코루틴 시작 전 로그
            StartCoroutine(SlideShowCoroutine());
        }
        else
        {
            Debug.Log("Selected slide index not found in PlayerPrefs");
        }
    }

    private void OnButtonClick()
    {
        if (!gameIntroShown)
        {
            // 버튼을 클릭하면 나머지 활성화
            gameIntroShown = true;
            timer.SetActive(true);
            ui.SetActive(true);
            students.SetActive(true);
        }
    }

    void InitializeButtonScores()
    {
        // 예시: 슬라이드마다 첫 번째 버튼은 10점, 두 번째 버튼은 5점
        buttonScores = new int[,] {
            { 1, 0 },
            { 0, 1 },
            { 1, 0 },
            { 1, 0 },
            { 0, 1 },
            { 0, 1 },
            { 1, 0 },
            { 0, 1 },
        };
    }

    IEnumerator SlideShowCoroutine()
    {
        Debug.Log("SlideShowCoroutine started"); // 코루틴 시작 시 출력
        float timer = 60.0f; // 1분 타이머
        float interval = timer / 8;

        while (timer > 0)
        {
            Debug.Log("SlideShowCoroutine running"); // 코루틴이 실행 중임을 나타냄
                                                     // 디버그 메시지 추가
            Debug.Log("Current Slide Index: " + currentSlideIndex);
            int currentSlide = currentSlideIndex % slideSprites.Length;
            Debug.Log("Current Slide (Calculated): " + currentSlide);

            InitializeButtonContents(); // 슬라이드마다 버튼 내용 업데이트
            yield return new WaitForSeconds(interval);
            timer -= interval;
            currentSlideIndex++;
            if (currentSlideIndex >= slideSprites.Length)
            {
                currentSlideIndex = 0;
            }
        }

        Debug.Log("SlideShowCoroutine ended"); // 코루틴 종료 시 출력
        EndPresentation();
    }

    public void OnOptionButtonClicked(int buttonIndex)
    {
        int slideIndex = currentSlideIndex % 8;
        int score = buttonScores[slideIndex, buttonIndex];
        totalScore += score; // 선택된 버튼의 점수 추가
        Debug.Log($"slideIndex: {slideIndex} / score: {score} / total : {totalScore}");
        foreach (Button button in optionButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    // 버튼 내용 초기화 함수
    void InitializeButtonContents()
    {
        string[] buttonTexts = {
            "안녕하세요. 발표를 맡은 한국대학교 3학년 [이름]입니다. 지금부터 발표를 시작하겠습니다.", "한국대학교의 역사를 주제로 지금까지 발표를 들어주셔서 감사합니다.",
            "한국대학교는 500여개의 학과를 처음부터 개설하여 시작했습니다.", "한국대학교는 한국 전쟁 이후 교육의 필요성이 강조되며 설립되었습니다.",
            "1960년대에는 교육의 확장과 질 향상에 주력했습니다.", "교육의 질 향상은 2000년대에 이른 지금도 아직 이루지 못했습니다.",
            "1970년대부터 80년대까지는 연구 성과 이룩에 힘썼습니다.", "1970년대부터 80년대까지는 아무 일도 없었습니다.",
            "1990년대에는 오히려 대학 인프라가 퇴화하는 사건들이 발생했습니다.", "1990년대에는 대학 인프라 강화를 통한 학생들의 여건 개선이 이루어졌습니다.",
            "2000년대 이후에는 금전적 횡령이 빈번히 일어났습니다.", "2000년대 이후에는 국제적으로 주목받는 대학으로 도약했습니다.",
            "한국대학교는 국내 대표 대학을 넘어 세계적인 대약으로 도약중입니다.", "한국대학교의 미래는 암울합니다.",
            "질문은 사절하겠습니다.", "지금까지 발표를 들어주셔서 감사합니다.",
        };

        // 현재 슬라이드 인덱스 계산
        int currentSlide = currentSlideIndex % slideSprites.Length;
        slideImage.sprite = slideSprites[currentSlide];

        int rowIndex = currentSlideIndex % 8; // 각 행을 결정하는 인덱스
        for (int i = 0; i < optionButtons.Length; i++)
        {
            string buttonText = buttonTexts[rowIndex * 2 + i]; // 행에 따른 버튼 텍스트
            Debug.Log("Button " + i + " Text for Slide " + currentSlideIndex + ": " + buttonText);
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
            optionButtons[i].gameObject.SetActive(true);
        }
    }

    void EndPresentation()
    {
        bool isWin = totalScore >= 5; // 5점 이상이면 승리, 아니면 실패

        DataController.instance.SetProgress(Progress.stage3_afterMiniGame, isWin);

        resultPanel.SetActive(true);
        resultPanel.transform.GetChild(0).GetComponent<Image>().sprite = isWin ? clearImg : failImg;

        Sfx sfx = isWin ? Sfx.success : Sfx.fail;
        SoundManager.instance.PlaySFX(sfx);

        // 최종 점수 로그 출력
        Debug.Log("EndMiniGame " + isWin.ToString() + ". Total Score: " + totalScore);
    }

}