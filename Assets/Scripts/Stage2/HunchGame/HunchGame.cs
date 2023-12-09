using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunchGame : S2_Minigame
{
    List<StudentUnit> units = new List<StudentUnit>();
    int currNum = 1;
    int maxNum = 6;
    Coroutine gameCoroutine;

    bool isGameDone = false;

    int userNum;

    public override void SetGame(int startIndex)
    {
        base.SetGame(startIndex);

        foreach (StudentUnit unit in GetManager().units)
        {
            units.Add(unit);
        }
        units.Remove(GetManager().playerUnit);

        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OnClickStart()
    {
        StudentUnit startUnit = GetManager().units[startIndex];

        if (startUnit != GetManager().playerUnit)
        {
            startUnit.SayNumber(1);
            units.Remove(startUnit);
            currNum++;
        }

        gameCoroutine = StartCoroutine(Game());
    }

    IEnumerator Game()
    {
        StudentUnit curUnit = null;

        while (currNum < maxNum)
        {
            float interval = Random.Range(0.5f, currNum);
            yield return new WaitForSeconds(interval);
            curUnit = GetRandomUnit();
            curUnit.SayNumber(currNum++);

            yield return null;
        }

        if (userNum == 0)
            base.EndGame(false, GetManager().playerUnit);
        else
            base.EndGame(true, units[0]);
    }

    StudentUnit GetRandomUnit()
    {
        int ranIndex = Random.Range(0, units.Count);
        StudentUnit unit = units[ranIndex];

        units.RemoveAt(ranIndex);

        return unit;
    }

    // 유저 입력 버튼에 연결
    public void OnClickButton(int num)
    {
        if (userNum > 0 || isGameDone) return;

        SoundManager.instance.PlaySFX(Sfx.button_ui);

        GetManager().playerUnit.SayNumber(num);
        userNum = num;

        if (num == currNum)
        {
            // 유저 승
            Debug.Log("유저 승");
            currNum++;
        }
        else
        {
            // 유저 패
            StopCoroutine(gameCoroutine);
            Debug.Log("유저 패");
            base.EndGame(false, GetManager().playerUnit);
        }
    }
}
