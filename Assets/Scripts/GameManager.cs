using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [System.NonSerialized] public List<CardData> cardDataList = new List<CardData>();
    [System.NonSerialized] public bool bEndGame = true;
    [SerializeField] Button backButton;


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

    public void EnablebackButton(bool b) 
    {
        backButton.gameObject.SetActive(b);
    }
    public void SaveCard(string spriteName, bool isMatched, bool isFlipped)
    {   
        CardData cardData = new CardData();
        cardData.spriteName = spriteName;
        cardData.isMatched = isMatched;
        cardData.isFlipped = isFlipped;
        cardDataList.Add(cardData); 
    }

    public void LoadGame()
    {
        GameData data = GetComponent<SaveLoadManager>().LoadGame();

        if (data != null) //continue game 
        {
            bEndGame = false;
            ScoreManager.Instance.SetScore(data.score);
            Timer.Instance.LastTime = data.gameTime;
            CardManager.Instance.columns = data.columns;
            CardManager.Instance.rows = data.rows;

            for (int i = 0; i < data.cardDataList.Count; i++)
            {
                SaveCard(data.cardDataList[i].spriteName, data.cardDataList[i].isMatched, data.cardDataList[i].isFlipped);
            }
            EnablebackButton(true);
        }
        else
        {
            bEndGame = true;
        }
    }

    private void OnDestroy()
    {
        if (bEndGame)
        {
            SaveLoadManager.Instance.ClearSaveData();
            return;
        }
        SaveLoadManager.Instance.SaveGame(
            ScoreManager.Instance.Score, 
            Timer.Instance.elapsedTime,
            CardManager.Instance.columns,
            CardManager.Instance.rows,
            cardDataList);
    }
}
