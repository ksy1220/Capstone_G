using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BunnyMotion
{
    bunny = 0,
    carrot = 1,
    shaking = 2
}

public class BunnyGame : S2_Minigame
{
    public Sprite carrotImg, shakingImg, bunnyImg;

    [SerializeField]
    BunnyIcon bunnyIcon;

    [SerializeField]
    BunnyGameButtonController buttonController;
    List<BunnyGameUnit> bunnyUnits = new List<BunnyGameUnit>();

    BunnyGameUnit currBunnyUnit;
    Coroutine coroutine = null;
    int maxBunnyIndex;
    int playerIndex;
    BunnyMotion correctInput;
    float interval = 1.2f;

    int roundCount = 0;
    int maxRound = 10;

    public override void SetGame(int startIndex)
    {
        base.SetGame(startIndex);

        Debug.Log("bunny game start");
        StudentUnit[] units = GetManager().units;

        for (int i = 0; i < units.Length; i++)
        {
            StudentUnit unit = units[i];
            if (unit.gameObject.GetComponent<BunnyGameUnit>()) continue;

            BunnyGameUnit bunnyUnit = unit.gameObject.AddComponent<BunnyGameUnit>();
            bunnyUnit.SetBunnyGameUnit(this, bunnyIcon, i);
            bunnyUnits.Add(bunnyUnit);
        }

        buttonController.SetBunnyGameManager(this, interval);
        playerIndex = GetManager().playerIndex;
        maxBunnyIndex = bunnyUnits.Count;

        bunnyUnits[startIndex].DoMotion(BunnyMotion.bunny);
    }

    public BunnyGameUnit GetRandomUnit()
    {
        return bunnyUnits[Random.Range(0, bunnyUnits.Count)];
    }

    public void MoveBunny()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(NotifyNextMotion());
    }

    IEnumerator NotifyNextMotion()
    {
        roundCount++;
        yield return new WaitForSeconds(interval);

        currBunnyUnit = GetRandomUnit();
        bunnyIcon.SetPosition(currBunnyUnit.transform);

        int bunnyIndex = currBunnyUnit.index;

        yield return new WaitForSeconds(interval);

        // 임의의 학생 패
        if (roundCount >= maxRound && bunnyIndex != playerIndex)
        {
            yield return new WaitForSeconds(interval);
            EndGame(true, bunnyUnits[bunnyIndex].GetComponent<StudentUnit>());
            yield break;
        }

        for (int i = 0; i < bunnyUnits.Count; i++)
        {
            if (i == playerIndex)
                correctInput = SetCorrectMotion(bunnyIndex, i);

            else
            {
                BunnyGameUnit unit = bunnyUnits[i];
                BunnyMotion nextMotion = SetCorrectMotion(bunnyIndex, i);
                unit.DoMotion(nextMotion);
            }
        }

        // 유저 입력 가능
        buttonController.EnableButtons();
    }

    BunnyMotion SetCorrectMotion(int bunnyIndex, int i)
    {
        if (i == bunnyIndex)
            return BunnyMotion.bunny;

        if (bunnyIndex == 0 && i == maxBunnyIndex - 1)
            return BunnyMotion.carrot;
        if (i == bunnyIndex - 1 || i == (bunnyIndex + 1) % maxBunnyIndex)
            return BunnyMotion.carrot;

        return BunnyMotion.shaking;
    }

    public void CheckPlayerInput(BunnyMotion playerInput)
    {
        if (correctInput != playerInput)
        {
            Debug.Log($"땡! / 정답: {correctInput.ToString()}");
        }
        else
        {
            Debug.Log($"정답");
        }
    }

}
