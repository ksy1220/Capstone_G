using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [SerializeField]
    List<Sprite> characterSprites;

    List<string> names = new List<string> { "선배1", "대학친구1", "대학친구2", "대학친구3", "대학친구4", "대학교수1", "대학교수2", "대학교수3", "팀원1", "팀원2", "팀원3", "동아리장", "동아리원" };
    public static SpriteController GetSpriteController()
    {
        return Resources.Load<SpriteController>("SpriteController");
    }

    public Sprite GetSprite(string charName)
    {
        int index = names.IndexOf(charName);

        return index > -1 ? characterSprites[index] : null;
    }
}
