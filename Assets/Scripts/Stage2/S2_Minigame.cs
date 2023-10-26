using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2_Minigame : MonoBehaviour
{
    S2_MinigameManager minigameManager;

    public void SetManager(S2_MinigameManager minigameManager)
    {
        this.minigameManager = minigameManager;
    }

    protected void EndGame(bool isWin)
    {
        Debug.Log($"End game: win? {isWin}");
        minigameManager.StartNextGame();
    }
}
