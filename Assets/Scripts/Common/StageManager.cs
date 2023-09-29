using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    스테이지의 대사 및 행동을 관리합니다.
    상속받아 사용하는 클래스
*/
public class StageManager : MonoBehaviour
{
    public static StageManager instance = null;
    protected DialogueController dialogueController { get { return DialogueController.instance; } }

    protected virtual void Awake()
    {
        instance = this;
    }

    public virtual void DoAction(string action)
    {

    }
}
