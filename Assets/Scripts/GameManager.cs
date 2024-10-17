using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    List<CardData> cardDataList = new List<CardData>();
    [System.NonSerialized] public bool bEndGame = true;


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
    public void SaveGame()
    {
        //List<CardData> cardDataList = new List<CardData>();

        //foreach (Card card in cards)
        //{
        //    cardDataList.Add(new CardData
        //    {
        //        id = card.id,
        //        isMatched = card.isMatched,
        //        isFlipped = card.isFlipped
        //    });
        //}

        //SaveLoadManager.SaveGame(score, gameTime, cardDataList);
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

            //for (int i = 0; i < data.cardDataList.Count; i++)
            //{
            //    Card card = cards.Find(c => c.id == data.cardDataList[i].id);
            //    if (card != null)
            //    {
            //        card.isMatched = data.cardDataList[i].isMatched;
            //        card.isFlipped = data.cardDataList[i].isFlipped;
            //    }
            //}
            Debug.Log("datadatadatadata");

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
