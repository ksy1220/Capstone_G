using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BRGameButton : MonoBehaviour
{
    TextMeshProUGUI tmp;
    Image image;
    int index;
    Button nextButton;
    bool isOn = false;
    string text;
    BRGameButtonController controller;
    Button button;

    void Awake()
    {
        controller = transform.parent.GetComponent<BRGameButtonController>();

        tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        image = GetComponent<Image>();
        index = transform.GetSiblingIndex();
        GetComponent<Button>().onClick.AddListener(OnClickButton);
        nextButton = transform.parent.GetChild(Mathf.Min(2, index + 1)).GetComponent<Button>();
        button = GetComponent<Button>();
    }

    void OnEnable()
    {
        Init();
    }

    void Init()
    {
        isOn = false;
        button.interactable = index == 0;
        image.color = Color.white;
    }

    public void SetText(string text)
    {
        if (text.CompareTo("31") > 0)
        {
            tmp.text = "";
            button.interactable = false;
        }
        else
            tmp.text = text;

        this.text = text;
    }

    public void OnClickButton()
    {
        if (isOn) return;

        isOn = true;
        image.color = Color.blue;

        controller.SayNumber(text);

        if (nextButton != this && text != "31")
            nextButton.interactable = true;
    }
}
