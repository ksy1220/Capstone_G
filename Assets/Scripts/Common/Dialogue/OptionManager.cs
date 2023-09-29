using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    고른 선택지에 맞는 액션을 취합니다.
    상속받아 사용하는 클래스
*/
public class OptionManager : MonoBehaviour
{
    public static OptionManager instance;
    protected virtual void Awake()
    {
        instance = this;
    }

    public virtual void TakeAction(string optionName, int index)
    {

    }
}
