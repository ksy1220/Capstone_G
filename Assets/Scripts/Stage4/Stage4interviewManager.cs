using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4interviewManager : StageManager
{
    [SerializeField]
    GameObject RoomBG, WaitingBG, InterviewBG1, InterviewBG2, InterviewBG3, library;
    [SerializeField]
    GameObject BackgroundCanvas;
    GameObject currentBG;

    void Start()
    {
        StartStage4();
    }

    void StartStage4()
    {
        dialogueController.StartDialogue("interview1");
        ChangeBG(library);
    }

    public override void DoAction(string action)
    {
        Debug.Log($"Action: {action}");
        switch (action)
        {
            case "ToInterviewWaiting":
                ChangeBG(WaitingBG);
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
