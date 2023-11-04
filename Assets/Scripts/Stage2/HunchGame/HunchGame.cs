using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunchGame : S2_Minigame
{
    List<StudentUnit> units = new List<StudentUnit>();
    int currNum = 1;
    int maxNum = 6;
    // 유닛끼리 같은 숫자 외칠 확률 (임시)
    float prob_unitsTroll = 0.1f;
    Coroutine gameCoroutine;

    bool isGameDone = false;

    int userNum;

    void Start()
    {
        foreach (StudentUnit unit in GetManager().units)
        {
            units.Add(unit);
        }
        units.Remove(GetManager().playerUnit);

        gameCoroutine = StartCoroutine(Game());
    }

    /*
        to do: 타이밍 조절
        예) 4 -> 5 외치는 데 오래 걸림 (어차피 5번은 빨리 외쳐야 함)
            다음 숫자가 너무 빨리 나올 때가 있음
    */
    IEnumerator Game()
    {
        StudentUnit curUnit = null;

        while (currNum <= maxNum)
        {
            // if (Random.Range(0.0f, 1.0f) <= prob_unitsTroll && units.Count >= 2)
            // {
            //     SaySameNumber();
            //     break;
            // }

            float interval = Random.Range(0.5f, currNum);
            yield return new WaitForSeconds(interval);
            curUnit = GetRandomUnit();
            curUnit.SayNumber(currNum++);

            yield return null;
        }

        base.EndGame(userNum != 0 && userNum != maxNum, curUnit);
    }

    void SaySameNumber()
    {
        GetRandomUnit().SayNumber(currNum);
        GetRandomUnit().SayNumber(currNum);

        Debug.Log("Said same number");
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
