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
    Dialogue dialogue;
    Button button;
    TextMeshProUGUI optionText;
    DialogueController dialogueController { get { return DialogueController.instance; } }
    StageManager stageManager { get { return StageManager.instance; } }

    void Awake()
    {
        index = transform.GetSiblingIndex();

        button = GetComponent<Button>();

        if (button == null)
            button = gameObject.AddComponent<Button>();

        button.onClick.AddListener(OnChooseOption);

        optionText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void SetOption(Dialogue dialogue)
    {
        this.dialogue = dialogue;
        this.optionText.text = dialogue.text;
    }

    void OnChooseOption()
    {
        StartDialogue();
        DoActionOnChoice();
    }

    void StartDialogue()
    {
        if (dialogueController == null)
        {
            Debug.LogError("DialogueController is null");
            return;
        }

        if (dialogue.nextCategory != "")
            dialogueController.StartDialogue(dialogue.nextCategory);
    }

    void DoActionOnChoice()
    {
        if (stageManager == null)
        {
            Debug.LogError("DialogueOption: StageManager instance is null");
            return;
        }
        stageManager.DoAction(dialogue.action);
    }
}
