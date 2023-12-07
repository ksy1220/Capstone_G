using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4interviewManager : StageManager
{
    [SerializeField]
    GameObject RoomBG, WaitingBG, InterviewBG1, InterviewBG2, InterviewBG3, library, Pass, Fail;
    [SerializeField]
    GameObject BackgroundCanvas;
    GameObject currentBG;


    private int interview_score = 100;

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
            case "ToInterview":
                ChangeBG(InterviewBG1);
                break;
            case "changeBG_i2":
                ChangeBG(InterviewBG2);
                break;
            case "changeBG_i3":
                ChangeBG(InterviewBG3);
                break;
            case "changeBG_i1":
                ChangeBG(InterviewBG1);
                break;
            case "up":
                interview_score += 30;
                break;
            case "down":
                interview_score -= 30;
                break;

            case "show_result":
                if (interview_score >= 190)
                {
                    ChangeBG(Pass);
                    break;
                }
                else
                {
                    ChangeBG(Fail);
                    break;
                }


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

    public void ShowEnding()
    {
        BackgroundCanvas.SetActive(false);
        DataController.instance.ShowEnding(interview_score >= 190);
    }
}
