using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueOption : MonoBehaviour
{
    int index;
    string optionName;
    Button button;

    void Awake()
    {
        index = transform.GetSiblingIndex();

        button = GetComponent<Button>();

        if (button == null)
            button = gameObject.AddComponent<Button>();

        button.onClick.AddListener(SaveChoice);
    }

    public void SetOptionName(string optionName)
    {
        this.optionName = optionName;
    }

    void SaveChoice()
    {

    }


}
