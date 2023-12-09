using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class ClassRegistrationTimer : MonoBehaviour
{
    public Text timerText; // 타이머 텍스트
    public Button[] registrationButtons; // 등록 버튼 배열
    public GameObject successPanel; // '수강 신청 완료' 패널
    public GameObject failurePanel; // '수강 인원이 다 찼습니다' 패널
    private TimeSpan activationTime = new TimeSpan(10, 0, 0); // 버튼 활성화 시간
    private TimeSpan currentTime;
    private bool buttonsActivated = false;
    private float timeBetweenButtons = 1.0f; // 버튼 클릭 사이의 시간 제한 (초)
    private float timeSinceLastButtonPressed; // 마지막 버튼 클릭 이후 경과 시간
    private HashSet<Button> failedButtons = new HashSet<Button>(); // 실패한 버튼들을 추적
    public Button successPanelConfirmButton; // '성공 패널의 확인 버튼'
    public Button failurePanelConfirmButton; // '실패 패널의 확인 버튼'
    public GameObject finalSuccessPanel; // 최종 성공 패널
    public GameObject finalFailurePanel; // 최종 실패 패널
    private HashSet<Button> clickedButtons = new HashSet<Button>(); // 클릭된 버튼들을 추적
    private int successCount = 0;







    void Start()
    {
        currentTime = new TimeSpan(9, 59, 55);
        UpdateTimerDisplay();
        SetButtonsActive(false);
        successPanel.SetActive(false);
        failurePanel.SetActive(false);
        successPanelConfirmButton.onClick.AddListener(ClosePanel);
        failurePanelConfirmButton.onClick.AddListener(ClosePanel);
    }

    void ClosePanel()
    {
        SoundManager.instance.PlaySFX(Sfx.button_ui);

        successPanel.SetActive(false);
        failurePanel.SetActive(false);

        // 패널을 닫은 후 최종 결과 평가
        EvaluateFinalResult();
    }




    void Update()
    {
        currentTime = currentTime.Add(TimeSpan.FromSeconds(Time.deltaTime));
        UpdateTimerDisplay();

        if (currentTime >= activationTime && !buttonsActivated)
        {
            SetButtonsActive(true);
            buttonsActivated = true;
            timeSinceLastButtonPressed = 0f; // 타이머 초기화
        }

        if (buttonsActivated)
        {
            timeSinceLastButtonPressed += Time.deltaTime;
        }

    }

    public void RegisterCourse(Button button)
    {
        if (failedButtons.Contains(button))
        {
            failurePanel.SetActive(true);
            return;
        }

        if (!clickedButtons.Contains(button))
        {

            clickedButtons.Add(button); // 클릭된 버튼 추가

            if (timeSinceLastButtonPressed <= timeBetweenButtons)
            {
                // 1초 이내에 클릭 시 성공 처리
                successPanel.SetActive(true);
                successCount++; // 성공 카운트 증가
                button.interactable = false; // 해당 버튼 비활성화
                button.GetComponentInChildren<TextMeshProUGUI>().text = "완료"; // 버튼 텍스트 변경
            }
            else
            {
                // 1초 이후 클릭 시 실패 처리
                failurePanel.SetActive(true);
                failedButtons.Add(button);
            }



            // 다음 버튼 클릭을 위해 타이머 재설정
            timeSinceLastButtonPressed = 0f;

            // 버튼 클릭 추적 및 최종 결과 평가
            if (!clickedButtons.Contains(button))
            {
                clickedButtons.Add(button);
                EvaluateFinalResult();
            }
        }

    }

    void EvaluateFinalResult()
    {
        // 모든 버튼이 클릭되었고, 최종 패널이 아직 표시되지 않았는지 확인
        if (clickedButtons.Count == registrationButtons.Length &&
            !finalSuccessPanel.activeSelf && !finalFailurePanel.activeSelf)
        {
            Progress nextProgress = DataController.instance.GetGameData().progress + 1;
            DataController.instance.SetProgress(nextProgress, successCount >= 5);

            // 최종 결과에 따라 적절한 최종 패널 표시
            if (successCount >= 5)
            {
                finalSuccessPanel.SetActive(true); // 최종 성공 패널 활성화
            }
            else
            {
                finalFailurePanel.SetActive(true); // 최종 실패 패널 활성화
            }
        }
    }





    void UpdateTimerDisplay()
    {
        timerText.text = currentTime.ToString("hh':'mm':'ss");
    }

    void SetButtonsActive(bool active)
    {
        foreach (Button btn in registrationButtons)
        {
            btn.interactable = active;
            if (active)
            {
                btn.onClick.AddListener(() => { RegisterCourse(btn); SoundManager.instance.PlaySFX(Sfx.button_click);});
            }
            else
            {
                btn.onClick.RemoveAllListeners();
            }
        }
    }

    public void ConfirmPanel()
    {
        successPanel.SetActive(false);
        failurePanel.SetActive(false);
    }





}