using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField]
    Button continueBtn, newGameBtn;

    void Start()
    {
        if (DataController.instance.GetGameData().progress == Progress.stage1)
            continueBtn.interactable = false;

        continueBtn.onClick.AddListener(() => OnClickStart(false));
        newGameBtn.onClick.AddListener(() => OnClickStart(true));
    }

    public void OnClickStart(bool isNewGame)
    {
        if (isNewGame)
            DataController.instance.LoadNewData();
        SceneController.LoadScene("Story");
    }
}
