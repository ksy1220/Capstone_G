using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRGameButtonController : MonoBehaviour
{
    [SerializeField]
    List<BRGameButton> buttons = new List<BRGameButton>();
    BRGame gameManager;

    [SerializeField]
    TextBalloon playerBalloon;

    void Awake()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void SetBRGameManager(BRGame gameManager)
    {
        this.gameManager = gameManager;
    }

    public void SetButtons(int startNum)
    {
        transform.parent.gameObject.SetActive(true);

        foreach (BRGameButton button in buttons)
        {
            Debug.Log(startNum);
            button.SetText(startNum++.ToString());
        }
    }

    public void SayNumber(string text)
    {
        playerBalloon.SetText(text);
        gameManager.IncreaseNumber();
    }

    public void OnClickDone()
    {
        gameManager.AfterUserInput();
        transform.parent.gameObject.SetActive(false);
    }
}
