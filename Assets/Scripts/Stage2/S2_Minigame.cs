using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S2_Minigame : MonoBehaviour
{
    S2_MinigameManager minigameManager;
    public int startIndex = 0;
    Toggle toggle;

    public void SetManager(S2_MinigameManager minigameManager)
    {
        this.minigameManager = minigameManager;
    }

    public S2_MinigameManager GetManager()
    {
        return minigameManager;
    }

    public virtual void SetGame(int startIndex)
    {
        this.startIndex = startIndex;

        if (!toggle)
            toggle = GetComponent<Toggle>();

        toggle.isOn = true;
    }

    public void EndGame(bool isWin, StudentUnit loserUnit)
    {
        Debug.Log($"End game: win? {isWin}");
        minigameManager.OnGameEnd(isWin, loserUnit.studentName);
    }
}
