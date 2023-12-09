using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[Serializable]
public class EndingInfo
{
    [TextArea]
    public string endingText;
    public Sprite background;
}

public class Ending : MonoBehaviour
{
    [SerializeField]
    EndingInfo[] endingInfos = new EndingInfo[6];

    [SerializeField]
    TextMeshProUGUI endingText;
    [SerializeField]
    Image background;
    [SerializeField]
    Button toMainButton;

    int endingIndex = -1;
    Button touchAreaButton;

    void Awake()
    {
        toMainButton.gameObject.SetActive(false);

        touchAreaButton = gameObject.AddComponent<Button>();
        touchAreaButton.onClick.AddListener(OnClickText);

        SoundManager.instance.PlayEndingBGM();
    }

    //     0           1            2              3            4          5
    // good pass / good fail / average pass / average fail / bad pass / bad fail
    public void SetEnding(int mentalIndex, bool interviewPassed)
    {
        if (mentalIndex >= 200)
            endingIndex = interviewPassed ? 0 : 1;

        else if (mentalIndex < 100)
            endingIndex = interviewPassed ? 4 : 5;

        else
            endingIndex = interviewPassed ? 2 : 3;

        SetEndingUI();
    }

    void SetEndingUI()
    {
        EndingInfo currEndingInfo = endingInfos[endingIndex];
        TypingEffect.instance.StartTyping(endingText, currEndingInfo.endingText, new TypingEffect.OnTypingEnd(ShowToMainButton));

        background.sprite = currEndingInfo.background;
    }

    void OnClickText()
    {
        TypingEffect.instance.EndTyping();
    }

    public void OnClickToMain()
    {
        DataController.instance.LoadNewData();
        SceneController.LoadScene("Main");

        SoundManager.instance.StopBGM();
    }

    void ShowToMainButton()
    {
        toMainButton.gameObject.SetActive(true);
    }
}
