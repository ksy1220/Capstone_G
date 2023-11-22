using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class TypingEffect : MonoBehaviour
{
    TextMeshProUGUI tmp;
    string text;
    bool isTyping = false;
    public bool IsTyping { get { return isTyping; } }

    Coroutine coroutine;
    static TypingEffect _instance = null;
    public static TypingEffect instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("@TypingEffect").AddComponent<TypingEffect>();
            }

            return _instance;
        }
    }

    public delegate void OnTypingEnd();
    OnTypingEnd callback;

    public void StartTyping(TextMeshProUGUI tmp, string text, OnTypingEnd callback = null)
    {
        if (isTyping)
            StopCoroutine(coroutine);

        this.tmp = tmp;
        this.text = text;
        this.callback = callback;

        coroutine = StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        StringBuilder sb = new StringBuilder();
        float interval = 0.05f;

        isTyping = true;

        for (int i = 0; i < text.Length; i++)
        {
            sb.Append(text[i]);
            tmp.text = sb.ToString();
            yield return new WaitForSeconds(interval);
        }

        EndTyping();
    }

    public void EndTyping()
    {
        if (isTyping)
            StopCoroutine(coroutine);

        tmp.text = text;
        isTyping = false;

        if (callback != null)
        {
            callback();
            callback = null;
        }
    }
}
