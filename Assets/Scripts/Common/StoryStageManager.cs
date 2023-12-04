using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryStageManager : StageManager
{
    [SerializeField]
    Sprite RoomBG, PCRoomBG, SchoolBG, Class1BG, Class2BG, Class3BG, Class4BG, RoadBG, BarBG, CoinSongBG, MTBG, CafeBG, StoreBG, LibraryBG, InterviewBG;

    [SerializeField]
    GameObject BackgroundCanvas;
    GameObject currentBG;
    Image BGImg;

    [SerializeField]
    string startingCategory;

    void Start()
    {
        BGImg = BackgroundCanvas.transform.GetChild(0).GetComponent<Image>();
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
