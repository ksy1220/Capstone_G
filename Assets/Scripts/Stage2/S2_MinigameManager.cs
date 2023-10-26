using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2_MinigameManager : MonoBehaviour
{
    int alcoholGauge = 5;
    public int AlcoholGauge
    {
        get { return alcoholGauge; }
        set
        {
            if (value <= 0)
                GameOver();
            else
                alcoholGauge = value;
        }
    }

    [SerializeField]
    GameObject[] MinigamePrefabs;

    GameObject currentGame;

    void Start()
    {
        foreach (GameObject minigame in MinigamePrefabs)
        {
            minigame.GetComponent<S2_Minigame>().SetManager(this);
        }
    }

    GameObject GetRandomGame()
    {
        return MinigamePrefabs[0];
    }

    public void StartNextGame()
    {
        Debug.Log("Start next game");
    }
    void GameOver()
    {

    }
}
