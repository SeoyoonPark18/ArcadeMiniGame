using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_ : MonoBehaviour
{
    public int currentLevel;
    public TMP_Text timerText;
    float _time; //선언 

    // 별
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    // 메뉴
    public GameObject R1Menu1;  // Round1-LiveMenu
    public GameObject R1Menu2;  // Round1-DieMenu

    // 목숨
    static public int Starpoint = 3;

    // 성공 확인용 bool
    bool isSuccess;
    bool timeStop = false;

    // 기게 판.
    public GameObject dollExit;


    void Start()
    {
        // 난이도 변수.
        currentLevel = MenuManager.level;


        SetTime();
        


        //별 활성화
        Star1.SetActive(true);
        Star2.SetActive(true);
        Star3.SetActive(true);

        R1Menu1.SetActive(false);
        R1Menu2.SetActive(false);


    }

    void Update()
    {
        if(timeStop == false)
        {
            _time -= Time.deltaTime;
        }   
        timerText.text = _time.ToString("F1");

        isSuccess = DollExit._isSuccess;

        // 별 표시.
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
                //실패 팝업
                R1Menu2.SetActive(true);
                timeStop = true;
                break;

        }

        //실패
        if (_time <= 0 && isSuccess == false)
        {
            Starpoint -= 1;
            SetTime();
        }

        if(isSuccess == true)
        {
            timeStop = true;
            R1Menu1.SetActive(true);
        }



    }

    void SetTime()
    {
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
 


}




