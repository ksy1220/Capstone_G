using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [SerializeField]
    Sprite[] characterSprites;

    public static SpriteController GetSpriteController()
    {
        return Resources.Load<SpriteController>("SpriteController");
    }

    public Sprite GetSprite(string charName)
    {
        if (charName == "나")
            return characterSprites[0];

        else if (charName == "대학친구1")
            return characterSprites[1];

        return null;
    }
}
