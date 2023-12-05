using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_ : MonoBehaviour
{
    public int currentLevel;
    public TMP_Text timerText;
    float _time; //���� 

    // ��
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    // �޴�
    public GameObject R1Menu1;  // Round1-LiveMenu
    public GameObject R1Menu2;  // Round1-DieMenu

    // ���
    static public int Starpoint = 3;

    // ���� Ȯ�ο� bool
    bool isSuccess;
    bool timeStop = false;

    // ��� ��.
    public GameObject dollExit;


    void Start()
    {
        // ���̵� ����.
        currentLevel = MenuManager.level;


        SetTime();
        


        //�� Ȱ��ȭ
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

        // �� ǥ��.
        switch (Starpoint)
        {
            case 2:
                Star3.SetActive(false);
                break;
            case 1:
                Star2.SetActive(false);
                break;
            case 0: // ����
                Star1.SetActive(false);
                //���� �˾�
                R1Menu2.SetActive(true);
                timeStop = true;
                break;

        }

        //����
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




