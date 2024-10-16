using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{

    TMP_Text Timer_Txt;

    private void Start()
    {
        //Debug.Log("");
        Timer_Txt = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        calculateGameTime();
    }
    void calculateGameTime()
    {
        if (CardManager.Instance.bEndGame) return;

        int seconds = (int)(Time.time % 60);
        int minutes = (int)(Time.time / 60);

        Timer_Txt.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
}
