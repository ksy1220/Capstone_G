using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SentenceManager : MonoBehaviour
{
    public GameObject sentencePrefab;
    public Transform sentencesContainer;

    private List<int> scoresPerSentence = new List<int>();
    private HashSet<int> selectedSentences = new HashSet<int>();
    private int currentButtonIndex;
    private int selectedSentenceIndex = -1; // 선택된 문장의 인덱스를 저장하는 변수
    private bool isSentenceHighlighted = false;


    public int CurrentButtonIndex { get { return currentButtonIndex; } }

    public void SetSentences(string[] sentences, int[] scores, int buttonIndex)
    {
        ClearSentences();
        currentButtonIndex = buttonIndex;

        float verticalSpacing = 0f; // 원하는 세로 간격 설정
        Vector3 spawnPosition = Vector3.zero; // 초기 생성 위치 설정

        for (int i = 0; i < sentences.Length; i++)
        {
            GameObject sentenceObj = Instantiate(sentencePrefab, sentencesContainer.transform, false);
            sentenceObj.transform.localPosition = spawnPosition; // 생성 위치 설정
                                                                 // 아래의 코드를 TextMeshPro 컴포넌트를 찾도록 수정합니다.
            TMPro.TextMeshProUGUI sentenceText = sentenceObj.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            sentenceText.text = sentences[i];

            int sentenceIndex = i;
            // Button 컴포넌트가 있는지 확인합니다.
            Button buttonComp = sentenceObj.GetComponentInChildren<Button>();
            if (buttonComp != null)
            {
                buttonComp.onClick.AddListener(() => OnSentenceClicked(sentenceIndex));
            }
            else
            {
                Debug.LogError("Button component is not found in the sentence prefab");
            }

            // 다음 프리팹을 생성할 때 세로 간격을 고려하여 spawnPosition 조절
            spawnPosition += new Vector3(0f, -verticalSpacing, 0f);
            scoresPerSentence.Add(scores[i]);
        }
    }

    private void OnSentenceClicked(int sentenceIndex)
    {
        SoundManager.instance.PlaySFX(Sfx.button_ui);

        // 클릭된 문장이 이미 하이라이트 상태인 경우 해제하고 리턴
        if (isSentenceHighlighted)
        {
            selectedSentences.Remove(sentenceIndex);
            HighlightSelectedSentence();
            isSentenceHighlighted = false; // 하이라이트 해제
            return;
        }

        // 레이캐스트를 생성하여 마우스 클릭 위치를 검사
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        // 레이캐스트 결과를 저장할 리스트
        List<RaycastResult> results = new List<RaycastResult>();

        // 레이캐스트 실행
        EventSystem.current.RaycastAll(eventData, results);

        // 클릭된 문장이 있는지 확인
        foreach (RaycastResult result in results)
        {
            // 클릭된 문장의 버튼 컴포넌트를 찾음
            Button buttonComp = result.gameObject.GetComponent<Button>();
            if (buttonComp != null)
            {
                // 클릭된 문장의 버튼이 있다면 선택 처리
                selectedSentences.Add(sentenceIndex);
                HighlightSelectedSentence();
                isSentenceHighlighted = true; // 하이라이트 설정
                return; // 클릭된 문장을 찾았으므로 종료
            }
        }
    }

    private void HighlightSelectedSentence()
    {
        foreach (Transform child in sentencesContainer)
        {
            // 모든 문장의 하이라이트를 해제
            Image highlightImage = child.GetComponent<Image>();
            if (highlightImage != null)
            {
                highlightImage.color = Color.white; // 기본색상으로 설정
            }
        }

        if (selectedSentences.Count > 0)
        {
            // 선택된 문장이 있다면 해당 문장의 하이라이트를 설정
            foreach (int sentenceIndex in selectedSentences)
            {
                Transform selectedChild = sentencesContainer.GetChild(sentenceIndex);
                Image highlightImage = selectedChild.GetComponent<Image>();
                if (highlightImage != null)
                {
                    highlightImage.color = Color.yellow; // 노란색으로 하이라이트 설정
                }
            }
        }

        // 디버그 로그를 추가하여 메서드가 호출되었음을 확인
        Debug.Log("HighlightSelectedSentence method called.");
    }

    public int CalculateButtonScore()
    {
        int score = 0;
        foreach (var index in selectedSentences)
        {
            score += scoresPerSentence[index];
        }
        selectedSentences.Clear();
        return score;
    }

    private void ClearSentences()
    {
        foreach (Transform child in sentencesContainer)
        {
            Destroy(child.gameObject);
        }
        scoresPerSentence.Clear();
        selectedSentences.Clear();
    }
}