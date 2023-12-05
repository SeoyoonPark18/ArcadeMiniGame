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

    //별
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;
    // 목숨
    static public int Starpoint = 3;


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


        //별 활성화
        Star1.SetActive(true);
        Star2.SetActive(true);
        Star3.SetActive(true);




    }

    void Update()
    {

       _time -= Time.deltaTime;
        timerText.text = _time.ToString("F1");

        //시간이 0이면.

        switch (Starpoint)
        {
            case 2:
                Star3.SetActive(false);
                break;
            case 1:
                Star2.SetActive(false);
                break;
            case 0: // 죽음
                Star1.SetActive(false);
                break;

        }

    }


    //테스트
    public void clikbb()
    {
        Starpoint -= 1;
        Debug.Log(Starpoint);
       

    }


}




