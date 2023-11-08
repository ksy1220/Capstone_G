using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyGameUnit : MonoBehaviour
{
    BunnyGame bunnyGameManager;
    BunnyIcon bunnyIcon;
    StudentUnit studentUnit;
    public int index;

    public void SetBunnyGameUnit(BunnyGame bunnyGame, BunnyIcon bunnyIcon, int index)
    {
        this.bunnyGameManager = bunnyGame;
        this.bunnyIcon = bunnyIcon;
        this.index = index;
    }

    void Awake()
    {
        studentUnit = GetComponent<StudentUnit>();
    }

    public void DoMotion(BunnyMotion motion)
    {
        switch (motion)
        {
            case BunnyMotion.bunny:
                DoBunny();
                break;
            case BunnyMotion.carrot:
                DoCarrot();
                break;
            case BunnyMotion.shaking:
                DoShaking();
                break;
        }
    }

    void DoBunny()
    {
        bunnyIcon.gameObject.SetActive(false);
        studentUnit.SayImage(bunnyGameManager.bunnyImg);

        bunnyGameManager.MoveBunny();
    }

    void DoCarrot()
    {
        studentUnit.SayImage(bunnyGameManager.carrotImg);
    }

    void DoShaking()
    {
        studentUnit.SayImage(bunnyGameManager.shakingImg);
    }

    void MoveBunny()
    {
        BunnyGameUnit unit = bunnyGameManager.GetRandomUnit();
        bunnyIcon.SetPosition(unit.transform);
        unit.Invoke("OnBunny", 1);
    }
}
