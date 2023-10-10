using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2Manager : StageManager
{
    [SerializeField]
    GameObject HomeBG, SchoolBG;
    [SerializeField]
    GameObject BackgroundCanvas, MiniGameCanvas;

    GameObject currentBG;

    void Start()
    {
        StartStage2();
    }

    void StartStage2()
    {
        dialogueController.StartDialogue("intro");
        ChangeBG(HomeBG);
    }

    public override void DoAction(string action)
    {
        Debug.Log($"Action: {action}");
        switch (action)
        {
            case "ToSchool":
                ChangeBG(SchoolBG);
                break;
            case "StartMiniGame":
                BackgroundCanvas.SetActive(false);
                MiniGameCanvas.SetActive(true);
                break;
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
