using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRGame : S2_Minigame
{
    StudentUnit[] units;
    [SerializeField]
    BRGameButtonController buttonController;
    int index = 0;

    int currentNum = 1;
    float interval = 0.5f;
    int playerIndex;

    public override void SetGame(int startIndex)
    {
        base.SetGame(startIndex);
        this.index = startIndex;

        units = GetManager().units;
        playerIndex = GetManager().playerIndex;
        buttonController.SetBRGameManager(this);

        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OnClickStart()
    {
        StartCoroutine(Game());
    }

    int GetRandomNum()
    {
        return Random.Range(1, 4);
    }

    public void IncreaseNumber()
    {
        currentNum++;
    }

    public void AfterUserInput(bool called31)
    {
        if (called31)
        {
            EndGame(false, GetManager().playerUnit);
        }
        else
        {
            // 게임 재개
            StartCoroutine(Game());
        }
    }

    IEnumerator Game()
    {
        while (currentNum <= 31)
        {
            // 플레이어
            if (index % 6 == playerIndex)
            {
                Debug.Log($"currentNum: {currentNum}");
                buttonController.SetButtons(currentNum);
                index++;
                yield break;
            }
            else
            {
                for (int i = 0; i < GetRandomNum(); i++)
                {
                    Debug.Log(currentNum);
                    units[index % 6].SayNumber(currentNum++);

                    if (currentNum >= 31) break;

                    yield return new WaitForSeconds(interval);
                }
            }
            index++;
            yield return new WaitForSeconds(interval);
        }

        Debug.Log("BR Done");
        Debug.Log($"loser: {index}");

        EndGame(true, units[(index - 1) % 6]);
    }
}
