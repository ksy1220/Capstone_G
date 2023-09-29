using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_OptionManager : OptionManager
{
    public override void DoAction(string optionName, int index)
    {
        switch (optionName)
        {
            case "continueTutorial":
                if (index == 0)
                    Debug.Log("야호 튜토리얼을 계속하자");
                else
                    Debug.Log("튜토리얼을 그만두자");
                break;
            default:
                Debug.Log("unknown option name");
                break;
        }
    }
}
