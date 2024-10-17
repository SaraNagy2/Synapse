using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    TMP_Text Score_Txt;
    int Score = 0;
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
    private void Start()
    {
        Score_Txt = GetComponent<TMP_Text>();
    }
    public void SetScore(int value) 
    {
        Score += value;

        if (Score < 0)
        {
            Score = 0;
        }

        Score_Txt.text = string.Format("Score: {0}", Score);
    }
    public void Reset()
    {
        Score = 0;
        Score_Txt.text = string.Format("Score: {0}", Score);
    }
}
