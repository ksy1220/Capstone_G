using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2Manager : StageManager
{
    [SerializeField]
    Sprite CoinSongBG, RoomBG, SchoolBG, PCRoomBG, ClassRoomBG, RoadBG;
    [SerializeField]
    GameObject BackgroundCanvas, MiniGameCanvas;
    S2_MinigameManager minigameManager;

    GameObject currentBG;
    Image BGImg;

    void Start()
    {
        minigameManager = MiniGameCanvas.GetComponent<S2_MinigameManager>();
        BGImg = BackgroundCanvas.transform.GetChild(0).GetComponent<Image>();
        StartStage2();
    }

    void StartStage2()
    {
        dialogueController.StartDialogue("intro");
        ChangeBG(CoinSongBG);
    }

    public override void DoAction(string action)
    {
        Debug.Log($"Action: {action}");
        switch (action)
        {
            case "ToCoinSong":
                ChangeBG(CoinSongBG);
                break;
            case "ToPCRoom":
                ChangeBG(PCRoomBG);
                break;
            case "ToRoom":
                ChangeBG(RoomBG);
                break;
            case "ToSchool":
                ChangeBG(SchoolBG);
                break;
            case "ToClassRoom":
                ChangeBG(ClassRoomBG);
                break;
            case "ToRoad":
                ChangeBG(RoadBG);
                break;
            case "OffBG":
                BackgroundCanvas.SetActive(false);
                break;
            case "StartMiniGame":
                BackgroundCanvas.SetActive(false);
                MiniGameCanvas.SetActive(true);
                break;
            case "StartNextGame":
                minigameManager.StartNextGame();
                break;
            default:
                Debug.Log("default action");
                break;
        }
    }

    public void AfterMiniGame()
    {
        dialogueController.StartDialogue("afterMiniGame");
    }

    void ChangeBG(Sprite sprite)
    {
        if (!BackgroundCanvas.activeSelf)
            BackgroundCanvas.SetActive(true);

        BGImg.sprite = sprite;
    }
}
