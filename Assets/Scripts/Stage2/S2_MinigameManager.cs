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

    public void OnGameEnd(bool isWin)
    {
        if (!isWin && ++alcoholGauge >= maxAlcoholGauge)
        {
            GameOver();
            return;
        }

        Debug.Log("Start randomGame dialogue");
        DialogueController.instance.StartDialogue("randomGame");
    }

    public void StartNextGame()
    {
        Debug.Log("Start next game");
        MinigamePrefabs[index++].SetGame(0);
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }
}
