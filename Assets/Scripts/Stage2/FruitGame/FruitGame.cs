using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FruitGame : S2_Minigame
{
    StudentUnit[] units;
    float interval = 0.4f;

    int currIndex = 1;      // 현재 인덱스
    int roundIndex = 0;     // 라운드 인덱스
    int maxRound = 12;      // 최대 라운드
    int unitIndex = 0;      // 학생 순서 인덱스
    [SerializeField]
    FruitGameButtonController buttonController;
    bool isIncreasing = true;

    int playerIndex;


    List<string> orderList = new List<string> { "딸기", "당근", "수박", "참외", "메론" };

    public override void SetGame(int startIndex)
    {
        base.SetGame(startIndex);
        this.unitIndex = startIndex;

        units = GetManager().units;
        buttonController.SetFruitGameManager(this);
        playerIndex = GetManager().playerIndex;

        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OnClickStart()
    {
        StartCoroutine(Game());
    }

    public void CheckUserInput(List<string> userInput)
    {
        List<string> correctAnswer = GetCorrectAnswer();

        string s = "";
        foreach (string u in userInput)
        {
            s += (u + " ");
        }
        Debug.Log($"user Input: {s}");

        if (userInput.SequenceEqual(correctAnswer))
        {
            // 학생 순서 넘기기
            unitIndex++;
            SetCurrIndex();

            // 게임 재개
            StartCoroutine(Game());
        }
        else
        {
            Debug.Log("땡");
            EndGame(false, GetManager().playerUnit);
        }
    }


    List<string> GetCorrectAnswer()
    {
        List<string> answerList = new List<string>();

        int orderIndex = 0;

        int maxIndex = currIndex <= 4 ? 4 : 8;

        for (int j = 0; j < maxIndex; j++)
        {
            if (j < 4 - currIndex || j >= 4 && j < 12 - currIndex)
                answerList.Add("짝");
            else
                answerList.Add(orderList[(orderIndex++) % 5]);
        }

        string answer = "";
        foreach (string s in answerList)
        {
            answer += (s + " ");
        }
        Debug.Log(answer);

        return answerList;
    }

    IEnumerator Game()
    {
        while (roundIndex <= maxRound)
        {
            if (unitIndex % 6 == playerIndex)
            {
                buttonController.transform.parent.gameObject.SetActive(true);
                yield break;
            }

            StudentUnit currUnit = units[unitIndex++ % 6];

            foreach (string s in GetCorrectAnswer())
            {
                currUnit.SayText(s);
                yield return new WaitForSeconds(interval);
            }

            SetCurrIndex();
            roundIndex++;
        }

        // 최대 라운드까지 플레이어가 살아남았을 경우
        SetCurrIndex();
        StudentUnit loserUnit = units[unitIndex % 6];
        foreach (string s in GetCorrectAnswer())
        {
            loserUnit.SayText(s);
            yield return new WaitForSeconds(interval);
        }
        Debug.Log("끝");
        EndGame(true, loserUnit);
    }

    void SetCurrIndex()
    {
        if (isIncreasing) currIndex++;
        else currIndex--;

        if (currIndex == 8) isIncreasing = false;
        else if (currIndex == 1) isIncreasing = true;
    }
}
