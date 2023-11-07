using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2_MinigameManager : MonoBehaviour
{
    int alcoholGauge = 0;
    int maxGameNum = 5;
    int maxAlcoholGauge = 3;

    [SerializeField]
    S2_Minigame[] MinigamePrefabs;

    public StudentUnit[] units;
    public StudentUnit playerUnit;

    GameObject currentGame;
    StudentUnit loserUnit;

    int index = 0;

    void Start()
    {
        playerUnit = units[1];

        foreach (S2_Minigame minigame in MinigamePrefabs)
        {
            minigame.SetManager(this);
        }

        StartNextGame();
    }

    public void OnGameEnd(bool isWin, StudentUnit loserUnit)
    {
        if (!isWin && ++alcoholGauge >= maxAlcoholGauge)
        {
            GameOver();
            return;
        }

        this.loserUnit = loserUnit;

        DialogueController.instance.StartDialogue("randomGame");
        DialogueUtils.ReplaceName(loserUnit.studentName);
    }

    public void StartNextGame()
    {
        Debug.Log("Start next game");
        int startIndex = loserUnit == null ? 0 : loserUnit.transform.GetSiblingIndex();
        Debug.Log($"loser unit index : {startIndex}");
        MinigamePrefabs[index++].SetGame(startIndex);
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }
}
