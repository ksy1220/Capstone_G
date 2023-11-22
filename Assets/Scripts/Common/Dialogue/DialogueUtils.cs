using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueUtils
{
    static List<Dialogue> currentDialogues;
    static int index = 0;

    public static void SetCurrentDialogues(List<Dialogue> dialogues)
    {
        if (dialogues == null)
        {
            Debug.LogError("DialogueUtils: invalid input");
            return;
        }

        index = 0;
        currentDialogues = dialogues;
    }

    public static void MoveNext()
    {
        if (currentDialogues == null)
        {
            Debug.LogError("DialogueUtils: currentDialogues is null");
            return;
        }

        if (DialogueController.instance == null)
        {
            Debug.LogError("DialogueUtils: DialogueController is null");
            return;
        }

        if (currentDialogues.Count <= index)
        {
            Debug.Log("End of dialogues");
            DialogueController.instance.EndDialogue();
            return;
        }

        if (TypingEffect.instance.IsTyping)
            TypingEffect.instance.EndTyping();
        else
        {
            Dialogue currentDialogue = GetNextDialogue();
            DialogueController.instance.SetDialogueUI(currentDialogue);
        }
    }

    public static Dialogue GetNextDialogue()
    {
        Debug.Log($"{index}: {currentDialogues[index].text}");

        return currentDialogues[index++];
    }

    public static void SkipDialogue()
    {
        while (index < currentDialogues.Count && !currentDialogues[index].type.Contains("선택지"))
        {
            MoveNext();
        }
    }

    static string beforeName = "{이름}";
    public static void ReplaceName(string name)
    {
        Debug.Log("Replace name");
        for (int i = 0; i < currentDialogues.Count; i++)
        {
            if (currentDialogues[i].text.Contains(beforeName))
            {
                currentDialogues[i].text = currentDialogues[i].text.Replace(beforeName, name);
            }
        }
        beforeName = name;
    }
}
