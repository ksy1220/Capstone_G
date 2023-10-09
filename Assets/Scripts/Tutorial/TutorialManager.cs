using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    튜토리얼에 나오는 대화와 액션 등을 관리합니다.
*/
public class TutorialManager : StageManager
{
    void Start()
    {
        new GameObject("@OptionManager").AddComponent<T_OptionManager>();

        dialogueController.StartDialogue("tutorial_0");
    }

    public override void DoAction(string action)
    {
        switch (action)
        {
            case "PrintHi":
                PrintHi();
                break;
        }
    }

    void PrintHi()
    {
        Debug.Log("Hi");
    }
}
