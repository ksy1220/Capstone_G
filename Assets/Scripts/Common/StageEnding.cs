using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageEnding : MonoBehaviour
{
    [SerializeField]
    Button toNextBtn;
    [SerializeField]
    TextMeshProUGUI endingTextTMP;

    string endingText;

    // Start is called before the first frame update
    void Start()
    {
        DataController.instance.ToNextStage();
        DataController.instance.SaveData();

        toNextBtn.onClick.AddListener(() => SceneController.LoadStoryScene());
        string mentalIndexText = "";
        int mentalIndex = DataController.instance.GetGameData().mentalIndex;

        if (mentalIndex >= 200)
            mentalIndexText = "좋음";
        else if (mentalIndex < 100)
            mentalIndexText = "나쁨";
        else
            mentalIndexText = "보통";

        endingText = $"스테이지 클리어\n\n현재 멘탈 지수: {mentalIndexText} ({mentalIndex})";

        TypingEffect.instance.StartTyping(endingTextTMP, endingText, new TypingEffect.OnTypingEnd(() => toNextBtn.gameObject.SetActive(true)));
    }




}
