using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{

    TMP_Text Timer_Txt;
    public static Timer Instance;
    [System.NonSerialized] public float elapsedTime;
    float startTime; 
    [System.NonSerialized] public float LastTime;

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
        Timer_Txt = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        calculateGameTime();
    }
    void calculateGameTime()
    {
        if (!CardManager.Instance || GameManager.Instance.bEndGame) return;

        // Calculate elapsed time since the timer was reset
        elapsedTime = Time.time - startTime + LastTime;

        int seconds = (int)(elapsedTime % 60);
        int minutes = (int)(elapsedTime / 60);

        Timer_Txt.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
    public void Reset()
    {
        startTime = Time.time;
        LastTime = 0;
    }
}
