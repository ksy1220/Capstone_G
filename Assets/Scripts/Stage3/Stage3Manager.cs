using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3Manager : StageManager
{
    [SerializeField] private Sprite RoomBG, ClassRoomBG;
    [SerializeField] private GameObject BackgroundCanvas, MiniGameCanvas;
    [SerializeField] private ResearchManager researchManager;
    [SerializeField] private SlidesManager slidesManager;
    [SerializeField] private PresentationManager presentationManager;

    private Image BGImg;

    void Start()
    {
        BGImg = BackgroundCanvas.transform.GetChild(0).GetComponent<Image>();

        // 바로 게임 시작
        BackgroundCanvas.SetActive(false);
        MiniGameCanvas.SetActive(true);
        researchManager.StartResearchGame();
    }

    void StartStage3()
    {
        dialogueController.StartDialogue("intro");
        ChangeBG(ClassRoomBG);
    }

    public override void DoAction(string action)
    {
        Debug.Log($"Action: {action}");
        switch (action)
        {
            case "ToClassroom1":
                ChangeBG(ClassRoomBG);
                break;
            case "ToRoom":
                ChangeBG(RoomBG);
                break;
            case "StartResearch":
                BackgroundCanvas.SetActive(false);
                MiniGameCanvas.SetActive(true);
                researchManager.StartResearchGame(); // Research 게임 시작
                break;
            case "StartSlides":
                slidesManager.StartSlidesGame(); // Slides 게임 시작
                break;
            case "StartPresentation":
                presentationManager.StartPresentationGame(); // Presentation 게임 시작
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
