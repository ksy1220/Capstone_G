using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameResultScreen : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => SceneController.LoadStoryScene());
    }
}
