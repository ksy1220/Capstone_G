using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentUnit : MonoBehaviour
{
    TextBalloon textBalloon;

    void Awake()
    {
        textBalloon = transform.GetChild(0).GetComponent<TextBalloon>();
    }

    public void SayNumber(int num)
    {
        textBalloon.SetText(num.ToString());
    }

    public void SayText(string text)
    {
        textBalloon.SetText(text);
    }
}
