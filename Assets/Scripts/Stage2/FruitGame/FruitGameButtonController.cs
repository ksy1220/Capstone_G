using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGameButtonController : MonoBehaviour
{
    FruitGame fruitGameManager;
    StudentUnit playerUnit;
    List<string> userInput = new List<string>();

    public void SetFruitGameManager(FruitGame fruitGameManager)
    {
        this.fruitGameManager = fruitGameManager;
        playerUnit = fruitGameManager.GetManager().playerUnit;
    }

    public void SayNumber(string text)
    {
        playerUnit.SayText(text);
        userInput.Add(text);
    }

    public void OnClickDone()
    {
        fruitGameManager.CheckUserInput(userInput);
        userInput = new List<string>();

        transform.parent.gameObject.SetActive(false);
    }
}
