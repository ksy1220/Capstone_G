using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryStageManager : StageManager
{
    [SerializeField]
    Sprite RoomBG, PCRoomBG, SchoolBG, Class1BG, Class2BG, Class3BG, Class4BG, RoadBG, BarBG, CoinSongBG, MTBG, CafeBG, StoreBG, LibraryBG, InterviewBG;

    [SerializeField]
    GameObject BackgroundCanvas, StageEndCanvas;
    GameObject currentBG;
    Image BGImg;

    void Start()
    {
        BGImg = BackgroundCanvas.transform.GetChild(0).GetComponent<Image>();
        StartDialogue();
    }

    void StartDialogue()
    {
        string startingCategory = "";

        switch (DataController.instance.GetGameData().progress)
        {
            // 1학년 시작
            case Progress.start:
            case Progress.stage1:
                startingCategory = "stage1";
                break;

            // 1학년 미니게임 이후
            case Progress.stage1_afterMiniGame:
                if (DataController.instance.GetGameData().miniGameSucceed)
                    startingCategory = "s1_success";
                else
                    startingCategory = "s1_fail";
                break;

            // 1학년 두 번째 미니게임 이후
            case Progress.stage1_afterSecondMiniGame:
                startingCategory = "s1_afterGame";
                break;

            // 2학년 시작
            case Progress.stage2:
                startingCategory = "stage2";
                break;

            // 2학년 미니게임 이후
            case Progress.stage2_afterMiniGame:
                if (DataController.instance.GetGameData().miniGameSucceed)
                    startingCategory = "s2_success";
                else
                    startingCategory = "s2_fail";
                break;


            // 3학년 시작
            case Progress.stage3:
                startingCategory = "stage3";
                break;

            // 3학년 미니게임 이후
            case Progress.stage3_afterMiniGame:
                if (DataController.instance.GetGameData().miniGameSucceed)
                    startingCategory = "s3_success";
                else
                    startingCategory = "s3_fail";
                break;

            // 4학년 시작
            case Progress.stage4:
                startingCategory = "stage4";
                break;
        }

        if (startingCategory == "")
            Debug.LogError("StoryStageManager : check starting category");
        else
            dialogueController.StartDialogue(startingCategory);
    }

    public override void DoAction(string action)
    {
        Debug.Log($"Action: {action}");
        switch (action)
        {
            case "ToRoom":
                ChangeBG(RoomBG);
                break;
            case "ToPCRoom":
                ChangeBG(PCRoomBG);
                break;
            case "ToSchool":
                ChangeBG(SchoolBG);
                break;
            case "ToClass1":
                ChangeBG(Class1BG);
                break;
            case "ToClass2":
                ChangeBG(Class2BG);
                break;
            case "ToClass3":
                ChangeBG(Class3BG);
                break;
            case "ToClass4":
                ChangeBG(Class4BG);
                break;
            case "ToRoad":
                ChangeBG(RoadBG);
                break;
            case "ToBar":
                ChangeBG(BarBG);
                break;
            case "ToCoinSong":
                ChangeBG(CoinSongBG);
                break;
            case "ToMT":
                ChangeBG(MTBG);
                break;
            case "ToCafe":
                ChangeBG(CafeBG);
                break;
            case "ToStore":
                ChangeBG(StoreBG);
                break;
            case "ToLibrary":
                ChangeBG(LibraryBG);
                break;
            case "ToInterviewWaiting":
                ChangeBG(InterviewBG);
                break;
            case "OffBG":
                BackgroundCanvas.SetActive(false);
                break;

            case "StartStage1":
                SceneController.LoadScene("Stage1");
                break;
            case "StartStage2":
                SceneController.LoadScene("Stage2");
                break;
            case "StartStage3":
                SceneController.LoadScene("Stage3");
                break;
            case "StartStage4":
                SceneController.LoadScene("stage4_game");
                break;
            case "EndStage":
                StageEndCanvas.SetActive(true);
                break;
            default:
                Debug.Log("default action");
                break;
        }
    }

    void ChangeBG(Sprite sprite)
    {
        if (!BackgroundCanvas.activeSelf)
            BackgroundCanvas.SetActive(true);

        BGImg.sprite = sprite;
    }
}
