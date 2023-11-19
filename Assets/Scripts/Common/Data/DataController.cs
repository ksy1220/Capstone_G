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
        }

        DontDestroyOnLoad(gameObject);
    }

    void Init()
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        LoadData();
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
            gameData = new GameData();
        }
    }

    public void SaveData()
    {
        if (gameData == null) return;

        string jsonData = JsonConvert.SerializeObject(gameData);

        File.WriteAllText(filePath, jsonData);

        Debug.Log("game data saved : " + filePath);
    }

    public void SetMentalIndex(int amount)
    {
        gameData.mentalIndex += amount;
        if (amount != 0)
        {
            Debug.Log($"멘탈지수: {amount} / 현재 멘탈지수: {gameData.mentalIndex}");
            SaveData();
        }
    }
}
