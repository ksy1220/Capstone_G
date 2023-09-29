using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    DialogueController와 연결,
    튜토리얼에 나오는 대화와 액션 등을 관리합니다.
*/
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance = null;

    // 현재 씬의 Dialogue Canvas에 부착된 컨트롤러
    [SerializeField]
    protected DialogueController dialogueController;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        dialogueController.StartDialogue("tutorial_0");
    }

    public void Action(string action)
    {
        switch (action)
        {
            case "PrintHi":
                PrintHi();
                break;
        }
    }

    public void PrintHi()
    {
        Debug.Log("Hi");
    }
}
