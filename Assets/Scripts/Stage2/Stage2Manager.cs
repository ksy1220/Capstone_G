using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2Manager : StageManager
{
    [SerializeField]
    Toggle HomeBG, SchoolBG;
    [SerializeField]
    GameObject BackgroundCanvas, MiniGameCanvas;

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
            default:
                Debug.Log("default action");
                break;
        }
    }

    void ChangeBG(Toggle BGToggle)
    {
        if (!BackgroundCanvas.activeSelf)
            BackgroundCanvas.SetActive(true);

        BGToggle.isOn = true;
    }
}
