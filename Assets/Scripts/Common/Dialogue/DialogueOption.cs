using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueOption : MonoBehaviour
{
    int index;
    string optionName;
    Button button;
    TextMeshProUGUI optionText;
    string nextCategory;
    DialogueController dialogueController;

    void Awake()
    {
        index = transform.GetSiblingIndex();

        button = GetComponent<Button>();

        if (button == null)
            button = gameObject.AddComponent<Button>();

        button.onClick.AddListener(OnChooseOption);

        optionText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void SetDialogueController(DialogueController dialogueController)
    {
        this.dialogueController = dialogueController;
    }

    public void SetOption(string optionName, string optionText, string nextCategory)
    {
        this.optionName = optionName;
        this.optionText.text = optionText;
        this.nextCategory = nextCategory;
    }

    void OnChooseOption()
    {
        dialogueController.StartDialogue(nextCategory);

        SaveChoice();
    }

    void SaveChoice()
    {
        Debug.Log($"option {index}: {optionName}");
    }


}
