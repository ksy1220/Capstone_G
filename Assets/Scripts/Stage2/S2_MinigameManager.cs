using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S2_MinigameManager : MonoBehaviour
{
    int alcoholGauge = 0;
    int maxAlcoholGauge = 3;

    [SerializeField]
    S2_Minigame[] MinigamePrefabs;

    public StudentUnit[] units;
    public StudentUnit playerUnit;
    public int playerIndex;

    GameObject currentGame;
    StudentUnit loserUnit;

    int index = 0;

    [SerializeField]
    Slider alcoholSlider;

    [SerializeField]
    Sprite clearImg, failImg;
    [SerializeField]
    GameObject ResultPanel;

    void Start()
    {
        playerIndex = playerUnit.transform.GetSiblingIndex();

        ShuffleGame();

        foreach (S2_Minigame minigame in MinigamePrefabs)
        {
            minigame.SetManager(this);
            Debug.Log(minigame.gameObject.name);
        }

        StartNextGame();
    }

    void ShuffleGame()
    {
        int ran1, ran2;
        int arrLength = MinigamePrefabs.Length;
        for (int i = 0; i < arrLength; i++)
        {
            ran1 = Random.Range(0, arrLength);
            ran2 = Random.Range(0, arrLength);

            S2_Minigame temp = MinigamePrefabs[ran1];
            MinigamePrefabs[ran1] = MinigamePrefabs[ran2];
            MinigamePrefabs[ran2] = temp;
        }
    }

    public void OnGameEnd(bool isWin, StudentUnit loserUnit)
    {
        if (!isWin)
        {
            alcoholSlider.value = ++alcoholGauge / (float)maxAlcoholGauge;

        }

        this.loserUnit = loserUnit;

        DialogueController.instance.StartDialogue("randomGame");
        DialogueUtils.ReplaceName(loserUnit.studentName);
    }

    public void StartNextGame()
    {
        if (alcoholGauge >= maxAlcoholGauge)
        {
            EndMiniGame(false);
            return;
        }

        if (index == MinigamePrefabs.Length)
        {
            EndMiniGame(true);
            return;
        }
        Debug.Log("Start next game");
        int startIndex = loserUnit == null ? playerIndex : loserUnit.transform.GetSiblingIndex();
        Debug.Log($"game index: {index} / loser unit index : {startIndex}");

        MinigamePrefabs[index++].SetGame(startIndex);
    }

    void EndMiniGame(bool isWin)
    {
        GetComponent<ToggleGroup>().SetAllTogglesOff();
        ResultPanel.SetActive(true);
        ResultPanel.transform.GetChild(0).GetComponent<Image>().sprite = isWin ? clearImg : failImg;

        Sfx sfx = isWin ? Sfx.success : Sfx.fail;
        SoundManager.instance.PlaySFX(sfx);

        Debug.Log("EndMiniGame " + isWin.ToString());

        DataController.instance.SetProgress(Progress.stage2_afterMiniGame, isWin);
    }

}
