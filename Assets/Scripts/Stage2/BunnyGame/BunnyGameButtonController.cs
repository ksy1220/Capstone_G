using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BunnyGameButtonController : MonoBehaviour
{
    BunnyGame bunnyGameManager;
    BunnyGameUnit playerUnit;
    List<Button> buttons = new List<Button>();
    bool clicked = false;

    // 입력 시간 제한
    float maxTime;

    void Awake()
    {
        transform.parent.gameObject.SetActive(false);

        foreach (Transform t in transform)
        {
            buttons.Add(t.GetComponent<Button>());
        }
    }

    IEnumerator StartTimer()
    {
        float time = 0.0f;
        while (time < maxTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        if (!clicked)
        {
            Debug.Log("시간 초과!");
        }
    }

    public void SetBunnyGameManager(BunnyGame bunnyGameManager, float interval)
    {
        this.bunnyGameManager = bunnyGameManager;
        playerUnit = bunnyGameManager.GetManager().playerUnit.GetComponent<BunnyGameUnit>();

        maxTime = interval;
    }

    public void EnableButtons()
    {
        clicked = false;
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }

        StartCoroutine(StartTimer());
    }

    public void DisableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }

    public void OnClickDone(int index)
    {
        clicked = true;
        DisableButtons();
        playerUnit.DoMotion((BunnyMotion)index);
        bunnyGameManager.CheckPlayerInput((BunnyMotion)index);
    }
}
