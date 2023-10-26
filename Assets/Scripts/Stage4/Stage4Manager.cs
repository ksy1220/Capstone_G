using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage4Manager : StageManager
{
    [SerializeField]
    GameObject RoomBG, WaitingBG, InterviewBG1, InterviewBG2, InterviewBG3;
    [SerializeField]
    GameObject BackgroundCanvas, HanoiGameCanvas, EmotionGameCanvas, WeatherGameCanvas;

    GameObject currentBG;

    void Start()
    {
        StartStage4();
    }

    void StartStage4()
    {
        dialogueController.StartDialogue("1");
        ChangeBG(RoomBG);
        /*dialogueController.StartDialogue("interview_waiting");
        ChangeBG(WaitingBG);*/
    }

    public override void DoAction(string action)
    {
        Debug.Log($"Action: {action}");
        switch (action)
        {
            case "hanoi":
                BackgroundCanvas.SetActive(false);
                HanoiGameCanvas.SetActive(true);
                break;
            case "emotion":
                BackgroundCanvas.SetActive(false);
                EmotionGameCanvas.SetActive(true);
                break;
            case "weather":
                BackgroundCanvas.SetActive(false);
                WeatherGameCanvas.SetActive(true);
                break;
            case "ToInterview":
                ChangeBG(InterviewBG1);
                break;
            // case "computer":
            //     ChangeBG(ComputerBG);
            //     break;
            case "PrintYes":
                Debug.Log("Yes");
                break;
            case "PrintNo":
                Debug.Log("No");
                break;
            default:
                Debug.Log("default action");
                break;
        }
    }

    void ChangeBG(GameObject BGObject)
    {
        if (!BackgroundCanvas.activeSelf)
            BackgroundCanvas.SetActive(true);

        if (currentBG != null)
            currentBG.SetActive(false);
        BGObject.SetActive(true);
        currentBG = BGObject;
    }
}
