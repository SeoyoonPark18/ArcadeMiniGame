using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public int currentLevel;
    public TMP_Text timerText;
    float _time; //선언 


    void Start()
    {
        currentLevel = MenuManager.level;

        if (currentLevel == 1)
        {
            _time = 90f;
        }
        else if (currentLevel == 2)
        {
            _time = 60f;
        }
        else if (currentLevel == 3)
        {
            _time = 30f;
        }
    }

    void Update()
    {

       _time -= Time.deltaTime;
        timerText.text = _time.ToString("F1");

        //시간이 0이면.
    }



}
