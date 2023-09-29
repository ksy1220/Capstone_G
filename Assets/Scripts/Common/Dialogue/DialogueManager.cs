using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueManager
{
    static Queue<Dialogue> currentDialogues;

    public static void SetCurrentDialogues(Queue<Dialogue> dialogues)
    {
        if (dialogues == null)
        {
            Debug.LogError("DialogueManager: invalid input");
            return;
        }

        currentDialogues = dialogues;
    }

    public static void MoveNext(DialogueController dialogueUI)
    {
        if (currentDialogues == null)
        {
            Debug.LogError("DialogueManager: currentDialogues is null");
            return;
        }

        if (currentDialogues.Count == 0)
        {
            Debug.Log("End of dialogues");
            dialogueUI.EndDialogue();
            return;
        }

        Dialogue currentDialogue = GetNextDialogue();
        dialogueUI.SetDialogueUI(currentDialogue);
    }

    public static Dialogue GetNextDialogue()
    {
        return currentDialogues.Dequeue();
    }

    public static void SkipDialogue()
    {
        currentDialogues = null;
    }
}
