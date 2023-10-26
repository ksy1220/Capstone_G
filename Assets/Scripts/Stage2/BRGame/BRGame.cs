using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRGame : MonoBehaviour
{
    [SerializeField]
    BRGameUnit[] units;
    [SerializeField]
    BRGameButtonController buttonController;
    int index = 0;

    int currentNum = 1;
    bool isUserInputDone = false;
    float interval = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        SetIndex(4);

        buttonController.SetBRGameManager(this);
        StartCoroutine(Game());
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    int GetRandomNum()
    {
        return Random.Range(1, 4);
    }

    public void IncreaseNumber()
    {
        currentNum++;
    }

    public void AfterUserInput()
    {
        // 게임 재개
        StartCoroutine(Game());
    }

    IEnumerator Game()
    {
        while (currentNum < 31)
        {
            // 플레이어
            if (index % 6 == 5)
            {
                Debug.Log($"currentNum: {currentNum}");
                buttonController.SetButtons(currentNum);
                index++;
                yield break;
            }
            else
            {
                for (int i = 0; i < GetRandomNum(); i++)
                {
                    Debug.Log(currentNum);
                    units[index % 6].SayNumber(currentNum++);

                    if (currentNum == 31) break;

                    yield return new WaitForSeconds(interval);
                }
            }
            index++;
            yield return new WaitForSeconds(interval);
        }

        if (index % 6 == 5)
        {
            buttonController.SetButtons(currentNum);
            yield break;
        }
        else
            units[index % 6].SayNumber(currentNum);

        Debug.Log("BR Done");
    }
}
