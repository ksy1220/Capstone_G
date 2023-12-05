using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DataController : MonoBehaviour
{
    static DataController _instance = null;

    public static DataController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("@DataController").AddComponent<DataController>();
            }
            return _instance;
        }
    }
    [SerializeField]
    GameData gameData;

    string filePath;
    string fileName = "gameData.json";

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Init();
        }
        else
        {
            Debug.Log("destroy data controller");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Init()
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        LoadData();
    }

    public void LoadNewData()
    {
        gameData = new GameData();
        SaveData(1);
    }

    public GameData GetGameData()
    {
        return gameData;
    }

    void LoadData()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

            gameData = (GameData)JsonConvert.DeserializeObject(jsonData, typeof(GameData));

            Debug.Log("game data loaded");
        }
        else
        {
            LoadNewData();
        }
    }

    public void SetProgress(Progress progress, bool miniGameSucceed = false)
    {
        gameData.progress = progress;
        gameData.miniGameSucceed = miniGameSucceed;
    }

    public void SaveData(int nextStageIndex)
    {
        if (gameData == null) return;

        gameData.progress = (Progress)(nextStageIndex * 10);

        string jsonData = JsonConvert.SerializeObject(gameData);

        File.WriteAllText(filePath, jsonData);

        Debug.Log("game data saved : " + filePath);
    }

    public void AddMentalIndex(int amount)
    {
        gameData.mentalIndex += amount;
        if (amount != 0)
        {
            Debug.Log($"멘탈지수: {amount} / 현재 멘탈지수: {gameData.mentalIndex}");
        }
    }

    public void ShowEnding(bool interviewPassed)
    {
        GameObject endingPrefab = Instantiate(Resources.Load<GameObject>("EndingCanvas"));

        Ending ending = endingPrefab.GetComponent<Ending>();

        ending.SetEnding(gameData.mentalIndex, interviewPassed);
    }
}
