using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchManager : MonoBehaviour
{

    public Button[] researchButtons;
    public GameObject textPanel;
    public SentenceManager sentenceManager;
    public Button completeButton;
    public Button nextStageButton;
    public SlidesManager slidesManager;

    private int totalScore = 0;
    private int completedButtons = 0;
    private bool[] buttonCompletionStates;
    private string[][] researchSentences = new string[][]
    
{
    new string[] // 주제 1에 대한 문장들
{
    "한국대학교는 1950년에 설립된 대학으로, 한국 교육계에서 높은 평가를 받고 있는 역사 깊은 기관 중 하나입니다.",
    "창립 당시, 한국대학교는 한국 전쟁 이후 교육의 필요성을 강조하며 설립되었습니다.",
    "한국대학교는 초기에는 일부 학부만을 가지고 시작하였으나, 시간이 흐름에 따라 학문 분야를 확장하고 다양한 학부와 전공을 도입하였습니다.",
    "이로써 학생들은 다양한 학문 분야에서 학습할 수 있게 되었고, 학교는 교육 다각화를 추구해왔습니다.",
    "1960년대에는 대학의 교육 프로그램이 확장되었으며, 학생들은 다양한 학문 분야를 탐구할 수 있게 되었습니다.",
    "이 시기에 학교는 학문적 다양성을 강화하고 교육의 질을 높이는데 주력하였습니다.",
    "1970년대에는 한국대학교가 연구와 혁신에 큰 비중을 두기 시작했습니다.",
    "학교는 다양한 연구 프로젝트를 시작하고, 기술 혁신을 촉진하여 국내외에서 주목받는 연구 성과를 이루어내었습니다.",
    "1990년대에는 대학 캠퍼스와 시설이 현대화되었고, 새로운 학술 시설과 도서관이 설립되어 학생들에게 학업을 지원하는데 큰 역할을 하였습니다.",
    "2000년대에는 한국대학교는 국제적으로도 주목받는 대학으로 성장하였습니다.",
    "국제 학술 협력 및 교류가 활발해지며, 학교는 국제적 입지를 높이는데 큰 기여를 하였습니다.",
    "대학의 캠퍼스는 아름다운 환경 속에 자리하고 있으며, 학술 시설과 도서관은 학생들의 학문적 탐구를 지원합니다.",
    "한국대학교는 학생들의 호기심을 존중하고, 학문적 성장과 리더십 발전을 위해 노력하고 있습니다.",
    "학교는 학생들이 미래에 성공적으로 기여할 수 있도록 훌륭한 교육을 제공하고, 국내외에서 높은 평가를 받고 있습니다."
},
    new string[]
{
    "한국대학교는 최근 인프라 향상과 주요 연구 업적을 바탕으로 세계대학랭킹에서 상위권으로 올라가는 데 성공했습니다.",
    "서울, 2023년 11월 23일 - 한국대학교가 교육 및 연구 분야에서의 높은 성과를 거두며 세계대학랭킹에서 상승세를 보이고 있습니다.",
    "과거 대학 캠퍼스의 근대화에 크게 기여한 것으로 평가받는 한국대학교는 지난 10년간 그 명성이 다소 빛을 바랜 듯한 행보를 보였습니다.",
    "하지만 최근 몇 년간의 노력과 투자로 학교는 세계적으로 인정받는 대학 중 하나로 자리매김하고 있습니다.",
    "한국대학교는 교육 및 연구 인프라 개선에 큰 투자를 진행하여 국제적으로 경쟁력을 향상시켰습니다.",
    "새로운 학술 시설과 연구 장비의 도입은 학교 내에서 혁신적인 연구를 가능하게 하였으며, 학생들과 교수진은 최신 기술 및 시설을 활용하여 더 나은 학습과 연구를 할 수 있게 되었습니다.",
    "이러한 인프라 개선은 학교의 연구 업적에도 긍정적인 영향을 미쳤습니다.",
    "최근 몇 년 동안, 한국대학교는 다양한 연구 분야에서 주요 업적을 이루어내며 학문적으로 높은 평가를 받았습니다.",
    "특히, 의학, 공학, 인문학 등 다양한 분야에서의 연구는 국제 학계에서 주목을 받았습니다.",
    "이러한 성과로 인해 한국대학교는 세계대학랭킹에서 상위권 대학 중 하나로 등극하였습니다.",
    "학교 관계자는 '우리 대학의 지속적인 노력과 투자가 학교의 교육과 연구에 긍정적인 영향을 미치고 있다'며 '앞으로도 지속적인 발전을 위해 노력할 것'이라고 밝혔습니다.",
    "또한, '학생들이 학문적 열정을 즐기며, 자신의 역량을 최대한 발휘할 수 있도록 환경을 조성하기 위해 힘쓸 것'이라고 덧붙였습니다.",
    "한국대학교는 학생들에게 더 나은 교육 환경을 제공하고, 국제 연구 커뮤니티에서의 영향력을 키우기 위해 계속해서 노력할 것으로 기대됩니다."
},
    new string[] // 주제 3에 대한 문장들
    {
    "1990년대, 한국대학교는 대학 캠퍼스와 시설 현대화를 위해 큰 노력을 기울였습니다.",
    "새로운 대학 시설을 건립하여 학생들에게 더 나은 학습 환경을 제공하고자 했고, 최신 기술을 도입하여 학교 인프라를 강화하였습니다.",
    "또한, 캠퍼스 내 휴게 공간과 식당을 개설하였으며, 도서관은 더 많은 학술 자료와 디지털 리소스를 구축하였습니다.",
    "학술 연구를 위한 실험실과 연구 시설을 현대화하여 연구 역량을 향상시켰습니다.",
    "캠퍼스 내에는 첨단 기술을 활용한 강의실과 연구 공간을 구축하였습니다.",
    "학교는 대학 캠퍼스 내의 생활 환경을 개선하고 학생들의 복지를 고려한 시설을 도입하였습니다.",
    "이러한 노력은 학생들의 학업 성취도 향상시키는 데 큰 기여를 하였습니다.",
    "또한, 캠퍼스 내 교통 및 주차 시설을 개선하여 학생들과 교직원의 이동을 편리하게 하였습니다.",
    "환경 친화적 시설을 도입하여 지속 가능한 캠퍼스 운영을 추구하였습니다.",
    "대학 캠퍼스 내의 보안 시설과 시스템을 강화하여 학교 커뮤니티의 안전을 보장하였습니다.",
    "캠퍼스 내 체육 시설을 개선하여 학생들의 스포츠 활동을 지원하였습니다.",
    "대학 캠퍼스와 시설 현대화는 학교의 교육과 연구 활동에 긍정적인 영향을 미쳤습니다.",
    "한국대학교는 앞으로도 캠퍼스와 시설을 지속적으로 현대화하여 학생들과 교직원 모두에게 더 나은 환경을 제공할 것입니다."
    }
};

private int[][] researchScores = new int[][]
{
    new int[] // 주제 1에 대한 각 문장의 점수
{
    1, 0, 1, 3, 1, 2, 3, 1, 2, 0, 3, 2, 0, 0
},
    new int[] // 주제 2에 대한 각 문장의 점수
    {
        1, 0, 1, 2, 1, 2, 3, 1, 0, 2, 0, 3, 2
    },
    new int[] // 주제 3에 대한 각 문장의 점수
    {
        2, 0, 2, 2, 1, 0, 2, 1, 3, 0, 2, 2, 1
    }
    // 추가 주제에 대한 점수 배열
};

    public void StartResearchGame()
{
    textPanel.SetActive(false);
    completeButton.gameObject.SetActive(false);
    nextStageButton.gameObject.SetActive(false);

    buttonCompletionStates = new bool[researchButtons.Length];
    for (int i = 0; i < researchButtons.Length; i++)
    {
        int index = i; // 임시 변수를 생성
        researchButtons[i].onClick.AddListener(() => OnResearchButtonClicked(index));
    }

    completeButton.onClick.AddListener(OnCompleteButtonClicked);
    nextStageButton.gameObject.SetActive(false); // 시작 시 NextStageButton을 비활성화
    nextStageButton.onClick.AddListener(StartNextGame); // NextStageButton 클릭 이벤트 설정
}

void OnResearchButtonClicked(int buttonIndex)
{
    textPanel.SetActive(true);
    completeButton.gameObject.SetActive(true);

    if (researchSentences != null && researchScores != null && sentenceManager != null)
    {
        if (buttonIndex < 0 || buttonIndex >= researchSentences.Length || buttonIndex >= researchScores.Length)
        {
            Debug.LogError("Button index is out of range!");
            return;
        }

        if (researchSentences[buttonIndex] == null || researchScores[buttonIndex] == null)
        {
            Debug.LogError("Sentences or scores array is null at index: " + buttonIndex);
            return;
        }

        sentenceManager.SetSentences(researchSentences[buttonIndex], researchScores[buttonIndex], buttonIndex);
    }
    else
    {
        Debug.LogError("One or more required components are not initialized.");
    }
}

void OnCompleteButtonClicked()
{
    // 기존 로직...
    int buttonScore = sentenceManager.CalculateButtonScore();
    totalScore += buttonScore;
    buttonCompletionStates[sentenceManager.CurrentButtonIndex] = true;
    completedButtons++;

    // 현재 주제의 텍스트 패널을 비활성화
    textPanel.SetActive(false);


    if (completedButtons == researchButtons.Length)
    {
        EndResearchGame();
    }
}

    void EndResearchGame()
    {
        Debug.Log("Total Score: " + totalScore);
        nextStageButton.gameObject.SetActive(true); // 모든 버튼이 완료되면 NextStageButton 활성화
    }

    void StartNextGame()
{
    if (slidesManager != null)
    {
        Debug.Log("Starting next game...");
        this.gameObject.SetActive(false);
        slidesManager.gameObject.SetActive(true);

        // 슬라이드 게임을 시작
        slidesManager.StartSlidesGame();
    }
    else
    {
        Debug.LogError("slidesManager is not set!");
    }
}
}