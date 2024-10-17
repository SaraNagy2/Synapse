using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance; 
    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //private void Start()
    //{
    //    List<CardData> cardDataList= new List<CardData> ();
    //    CardData cardData = new CardData();
    //    cardData.id = 0;
    //    cardData.isFlipped = false;
    //    cardData.isMatched = false;
    //    cardDataList.Add(cardData);
    //    SaveGame(20, 10, cardDataList);
    //}
    public void SaveGame(int score, float gameTime, int columns, int rows, List<CardData> cardDataList)
    {
        GameData data = new GameData
        {
            score = score,
            gameTime = gameTime,
            columns = columns,
            rows = rows,
            cardDataList = cardDataList
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved.");
    }
    public GameData LoadGame()
    {
        saveFilePath = Application.persistentDataPath + "/savedata.json"; // Path to save the file

        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game loaded.");
            return data;
        }

        Debug.Log("No save file found." + saveFilePath);
        return null; // Return null if no save file exists
    }

    public void ClearSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save data cleared.");
        }
    }
}
