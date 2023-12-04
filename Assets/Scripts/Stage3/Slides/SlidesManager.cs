using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlidesManager : MonoBehaviour
{
    public Image[] slides; // 슬라이드 이미지 배열
    public GameObject[] slideCovers; // 슬라이드 커버 배열
    public TextMeshProUGUI[] slideTexts; // 슬라이드 텍스트 배열
    public Button[] slideButtons; // 슬라이드 버튼 배열
    public Button goToPresentationButton;
    private int selectedElementIndex = -1;
    public PresentationManager presentationManager;

    public int SelectedElementIndex
    {
        get { return selectedElementIndex; }
    }
    

    // SlideSelected 이벤트 정의
    public delegate void SlideSelectedHandler(int slideIndex);
    public event SlideSelectedHandler OnSlideSelected;
    public void StartSlidesGame()
    {
        goToPresentationButton.gameObject.SetActive(false);

        // 모든 슬라이드 커버를 활성화하고 실제 슬라이드 이미지를 숨깁니다.
        foreach (var cover in slideCovers)
        {
            cover.SetActive(true);
        }
        foreach (var slide in slides)
        {
            slide.gameObject.SetActive(false);
        }
        foreach (var text in slideTexts)
        {
            text.gameObject.SetActive(false);
        }
        
        // 모든 슬라이드 버튼에 대해 리스너 추가
        for (int i = 0; i < slideButtons.Length; i++)
        {
            int index = i; // 현재 인덱스 캡처
            slideButtons[i].onClick.AddListener(() => SelectSlide(index));
        }

        // goToPresentationButton에 대한 리스너 추가
    goToPresentationButton.onClick.AddListener(StartNextGame);
    }

    public void SelectSlide(int index)
{
    Debug.Log("SelectSlide called: " + index);
    selectedElementIndex = index;

    // PlayerPrefs를 사용하여 선택된 슬라이드 인덱스 저장
    PlayerPrefs.SetInt("SelectedSlideIndex", index);
    PlayerPrefs.Save();

    // 모든 슬라이드 커버를 숨깁니다.
    foreach (var cover in slideCovers)
    {
        cover.SetActive(false);
    }

    // 모든 슬라이드 이미지를 활성화합니다.
    foreach (var slide in slides)
    {
        slide.gameObject.SetActive(true);
    }

    // 선택된 슬라이드의 텍스트를 활성화하고 나머지는 비활성화합니다.
    for (int i = 0; i < slideTexts.Length; i++)
    {
        slideTexts[i].gameObject.SetActive(i == index);
    }

    // 선택된 슬라이드에 "당첨" 텍스트를 설정합니다.
    if (index < slideTexts.Length)
    {
        slideTexts[index].text = "당첨";
    }

    goToPresentationButton.gameObject.SetActive(true);

    // 선택된 슬라이드 인덱스를 PresentationManager에 전달합니다.
    OnSlideSelected?.Invoke(index);
}

void StartNextGame()
{
    if (presentationManager != null)
    {
        Debug.Log("Starting next game...");
        this.gameObject.SetActive(false);
        presentationManager.gameObject.SetActive(true);

        // 슬라이드 게임을 시작
        presentationManager.StartPresentationGame();
    }
    else
    {
        Debug.LogError("presentationManager is not set!");
    }
}
}
