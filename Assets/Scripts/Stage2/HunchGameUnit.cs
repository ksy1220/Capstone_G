using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HunchGameUnit : StudentUnit
{
    TextMeshProUGUI text;
    void Awake()
    {
        text = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = "";
    }

    public void SayNumber(int num)
    {
        text.text = num.ToString();
        Debug.Log($"{name} : {num}");
    }
}
