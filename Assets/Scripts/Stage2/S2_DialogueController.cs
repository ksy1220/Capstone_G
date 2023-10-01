using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2_DialogueController : DialogueController
{
    [SerializeField]
    Sprite[] penguinSprites;

    protected override Sprite GetSprite(string charName, string spriteName)
    {
        if (charName == "펭순이")
        {
            if (spriteName == "")
                return penguinSprites[0];
        }
        return null;
    }
}
