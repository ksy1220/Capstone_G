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

        if (currentDialogues.Count == 0)
        {
            Debug.Log("End of dialogues");
            DialogueController.instance.EndDialogue();
            return;
        }

        Dialogue currentDialogue = GetNextDialogue();
        DialogueController.instance.SetDialogueUI(currentDialogue);
    }

    public static Dialogue GetNextDialogue()
    {
        return currentDialogues[index++];
    }

    public static void SkipDialogue()
    {
        while (currentDialogues.Count > 0 && !currentDialogues[index].type.Contains("선택지"))
        {
            MoveNext();
        }

    }
}
