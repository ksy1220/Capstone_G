using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [SerializeField]
    Sprite[] penguinSprites;

    public static SpriteController GetSpriteController()
    {
        return Resources.Load<SpriteController>("SpriteController");
    }

    public Sprite GetSprite(string charName, string spriteName)
    {
        if (charName == "펭순이")
        {
            switch (spriteName)
            {
                default:
                    return penguinSprites[0];
            }
        }
        return null;
    }
}
