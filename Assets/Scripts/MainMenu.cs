using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
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
    public void ChooseLayouts(int columns)
    {
        int rows = 0;
        if (columns == 2) rows = 2;
        if (columns == 4) rows = 4;
        if (columns == 5) rows = 4;
        if (columns == 6) rows = 5;
        if (columns == 8) rows = 5;


        GameManager.Instance.bEndGame = false;

        CardManager.Instance.InstantiateCards(columns,rows);
        
        CardManager.Instance.gameObject.SetActive(true);

        gameObject.SetActive(false);
        Timer.Instance.Reset();
        ScoreManager.Instance.Reset();
    }
}
