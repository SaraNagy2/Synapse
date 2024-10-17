using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int score;
    public float gameTime;
    public int columns;
    public int rows;
    public List<CardData> cardDataList; // This will hold the state of each card
}

[System.Serializable]
public class CardData
{
    public string spriteName; 
    public bool isMatched; // Is the card matched?
    public bool isFlipped; // Is the card currently flipped?
}