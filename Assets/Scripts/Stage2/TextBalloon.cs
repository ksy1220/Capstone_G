using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBalloon : MonoBehaviour
{
    CanvasGroup canvasGroup;
    TextMeshProUGUI tmp;
    float fadeTime = 1.0f;
    float colorPerFrame;

    Coroutine currentCoroutine = null;

    void Awake()
    {
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        colorPerFrame = 1 / fadeTime;

        gameObject.SetActive(false);
    }

    void Reset()
    {
        canvasGroup.alpha = 1;
        fadeTime = 1.0f;
    }

    public void SetText(string text)
    {
        tmp.text = text;
        gameObject.SetActive(true);

        Reset();

        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        while (fadeTime > 0)
        {
            fadeTime -= Time.deltaTime;
            canvasGroup.alpha -= colorPerFrame * Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
