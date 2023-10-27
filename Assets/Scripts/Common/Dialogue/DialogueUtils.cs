using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueUtils
{
    static Queue<Dialogue> currentDialogues;

    public static void SetCurrentDialogues(Queue<Dialogue> dialogues)
    {
        if (dialogues == null)
        {
            Debug.LogError("DialogueUtils: invalid input");
            return;
        }

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
        return currentDialogues.Dequeue();
    }

    public static void SkipDialogue()
    {
        while (currentDialogues.Count > 0 && !currentDialogues.Peek().type.Contains("선택지"))
        {
            MoveNext();
        }

    }
}
