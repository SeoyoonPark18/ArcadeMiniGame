using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{

    GameObject timerText;
    float time = 60.0f; //���� 


    // Start is called before the first frame update
    void Start()
    {
        this.timerText = GameObject.Find("Time");
    }

    // Update is called once per frame
    void Update()
    {
        this.time -= Time.deltaTime;
        this.timerText.GetComponent<TextMeshProUGUI>().text = this.time.ToString("F1"); // Null ����
    }


    //���̵� ���� ��ư �Լ�
    public void OnclickL1() 
    {
       //float time = 30.0f;  // Easy
    }
    public void OnclickL2()
    {
        //float time = 60.0f;  // Normal
    }
    public void OnclickL3()
    { 
        //float time = 90.0f;  // Hard

    }




 
   
   


}
