using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/*
    대화 내용에 따라 대화창을 세팅합니다.

    이 클래스를 상속받아 일부 함수를 오버라이드해서 사용,
    각 씬의 Dialogue Canvas에 부착해줍니다.
*/
public class DialogueController : MonoBehaviour
{
    public static DialogueController instance = null;

    [Header("UI")]

    // 캐릭터 대사 / 이름 / 알림창 텍스트
    [SerializeField]
    protected TextMeshProUGUI lineText;
    [SerializeField]
    protected TextMeshProUGUI nameText;
    [SerializeField]
    protected TextMeshProUGUI noticeText;

    // 캐릭터 이미지
    [SerializeField]
    protected Image characterImage;
    // 캐릭터 대사창 / 알림창 / 선택지창
    [SerializeField]
    protected Toggle characterLineToggle, noticeToggle, optionToggle;
    // 선택지 부모 트랜스폼
    [SerializeField]
    protected Transform optionsParent;
    // 터치 입력을 받는 패널
    [SerializeField]
    protected GameObject TouchPanel;
    [SerializeField]
    protected ToggleGroup dialougeToggleGroup;


    [Header("대화 파일")]
    // 사용할 csv 대화 파일 이름 
    [SerializeField]
    protected string CSVFileName;

    protected Dictionary<string, Queue<Dialogue>> dialogues;

    protected virtual void Awake()
    {
        instance = this;

        Button touchPanelButton = GetComponent<Button>();
        if (touchPanelButton == null)
            touchPanelButton = TouchPanel.AddComponent<Button>();

        touchPanelButton.onClick.AddListener(OnClickMoveNext);

        dialougeToggleGroup.allowSwitchOff = true;
        SetDialogues();

        ToggleInteractionOff();
    }

    // 토글 터치 후 꺼짐 방지
    void ToggleInteractionOff()
    {
        characterLineToggle.interactable = false;
        noticeToggle.interactable = false;
        optionToggle.interactable = false;
    }

    // 씬 전체에서 사용할 대사 불러오기
    protected void SetDialogues()
    {
        dialogues = CSVConverter.GetDialogues(CSVFileName);
    }

    // 카테고리 이름으로 대화 시작
    public void StartDialogue(string category)
    {
        DialogueUtils.SetCurrentDialogues(dialogues[category]);
        DialogueUtils.MoveNext();
    }

    // 대화창 세팅
    public void SetDialogueUI(Dialogue dialogue)
    {
        switch (dialogue.type)
        {
            case "대사":
                characterLineToggle.isOn = true;

                lineText.text = dialogue.text;
                nameText.text = dialogue.name;

                Sprite sprite = GetSprite(dialogue.name, dialogue.sprite);

                if (sprite == null)
                    characterImage.gameObject.SetActive(false);
                else
                {
                    characterImage.gameObject.SetActive(true);
                    characterImage.sprite = sprite;
                }

                SetTouchPanelOn();
                break;

            case "알림":
                noticeToggle.isOn = true;
                noticeText.text = dialogue.text;
                SetTouchPanelOn();
                break;

            case "선택지1":
            case "선택지2":
            case "선택지3":
                optionToggle.isOn = true;
                // 터치입력 받지 않기
                TouchPanel.SetActive(false);

                int optionNum = dialogue.type[3] - '0';
                int maxOptionNum = 3;

                for (int i = 0; i < maxOptionNum; i++)
                {
                    GameObject optionObject = optionsParent.GetChild(i).gameObject;
                    if (i < optionNum)
                    {
                        optionObject.GetComponent<DialogueOption>().SetOption(dialogue.optionName, dialogue.text, dialogue.nextCategory);
                        optionObject.SetActive(true);

                        if (i < optionNum - 1)
                            dialogue = DialogueUtils.GetNextDialogue();
                    }
                    else
                        optionObject.SetActive(false);
                }
                return;

            case "액션":
                break;

            default:
                Debug.LogError("DialogueController: invalid type");
                break;
        }

        if (dialogue.action != "")
        {
            DoAction(dialogue.action);
        }

        if (dialogue.nextCategory != "")
        {
            SetNextDialogues(dialogue.nextCategory);
        }
    }

    // 오버라이드 필요
    protected virtual void DoAction(string action)
    {

    }

    protected virtual Sprite GetSprite(string charName, string spriteName)
    {
        return null;
    }

    // 다음 대화 카테고리 시작
    void SetNextDialogues(string category)
    {
        DialogueUtils.SetCurrentDialogues(dialogues[category]);
    }

    void SetTouchPanelOn()
    {
        if (!TouchPanel.activeSelf)
            TouchPanel.SetActive(true);
    }

    // 터치 입력을 받는 패널의 버튼에 연결
    void OnClickMoveNext()
    {
        DialogueUtils.MoveNext();
    }

    // 대화 종료 후 모든 대화창 끄기
    public void EndDialogue()
    {
        dialougeToggleGroup.SetAllTogglesOff();
        TouchPanel.SetActive(false);
    }

}
