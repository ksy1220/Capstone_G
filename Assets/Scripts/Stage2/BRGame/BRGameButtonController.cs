using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRGameButtonController : MonoBehaviour
{
    [SerializeField]
    List<BRGameButton> buttons = new List<BRGameButton>();
    BRGame brGameManager;
    StudentUnit playerUnit;

    bool called31 = false;
    bool clicked = false;

    void Awake()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void SetBRGameManager(BRGame brGameManager)
    {
        this.brGameManager = brGameManager;
        playerUnit = brGameManager.GetManager().playerUnit;
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
        playerUnit.SayText(text);
        brGameManager.IncreaseNumber();

        if (text == "31")
            called31 = true;

        clicked = true;
    }

    public void OnClickDone()
    {
        if (!clicked) return;
        clicked = false;

        brGameManager.AfterUserInput(called31);
        transform.parent.gameObject.SetActive(false);
    }
}
