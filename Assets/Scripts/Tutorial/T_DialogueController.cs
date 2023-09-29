using UnityEngine;

/*
    (임시) 튜토리얼에서 사용할 DialogueController 예시
*/
public class T_DialogueController : DialogueController
{
    [SerializeField]
    Sprite[] penguinSprites;

    protected override void Action(string action)
    {
        TutorialManager.instance.Action(action);
    }

    protected override Sprite GetSprite(string charName, string spriteName)
    {
        if (charName == "주인공")
        {
            if (spriteName == "")
                return penguinSprites[0];
        }
        return null;
    }
}
