using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
    선택지 버튼에 부착합니다.
*/
public class DialogueOption : MonoBehaviour
{
    int index;
    string optionName;
    Button button;
    TextMeshProUGUI optionText;
    string nextCategory;
    DialogueController dialogueController { get { return DialogueController.instance; } }
    OptionManager optionManager { get { return OptionManager.instance; } }

    void Awake()
    {
        index = transform.GetSiblingIndex();

        button = GetComponent<Button>();

        if (button == null)
            button = gameObject.AddComponent<Button>();

        button.onClick.AddListener(OnChooseOption);

        optionText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void SetOption(string optionName, string optionText, string nextCategory)
    {
        this.optionName = optionName;
        this.optionText.text = optionText;
        this.nextCategory = nextCategory;
    }

    void OnChooseOption()
    {
        if (dialogueController == null)
        {
            Debug.LogError("DialogueController is null");
            return;
        }

        dialogueController.StartDialogue(nextCategory);
        TakeActionOnChoice();
        SaveChoice();
    }

    void TakeActionOnChoice()
    {
        if (optionManager == null)
        {
            Debug.LogError("OptionManager is null");
            return;
        }
        optionManager.TakeAction(optionName, index);
    }

    void SaveChoice()
    {

    }
}
