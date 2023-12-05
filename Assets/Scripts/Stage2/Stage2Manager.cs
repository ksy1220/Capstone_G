using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2Manager : StageManager
{
    [SerializeField]
    GameObject MiniGameCanvas;
    S2_MinigameManager minigameManager;

    GameObject currentBG;
    Image BGImg;

    void Start()
    {
        minigameManager = MiniGameCanvas.GetComponent<S2_MinigameManager>();
        MiniGameCanvas.SetActive(true);
    }


    public override void DoAction(string action)
    {
        Debug.Log($"Action: {action}");
        switch (action)
        {
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
        SceneController.LoadStoryScene();
    }
}
