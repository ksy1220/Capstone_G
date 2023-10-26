using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    스테이지의 대사 및 행동을 관리합니다.
    상속받아 사용하는 클래스
*/
public abstract class StageManager : MonoBehaviour
{
    public static StageManager instance = null;
    [SerializeField] protected DialogueController dialogueController;

    protected virtual void Awake()
    {
        instance = this;
    }

    public abstract void DoAction(string action);
}
