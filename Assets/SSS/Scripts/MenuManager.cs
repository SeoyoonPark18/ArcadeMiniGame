using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    

    // UI ������Ʈ ����
    public GameObject gameLabel1;  // Menu1
    public GameObject gameLabel2;  // Menu2
    public GameObject gameLabel3;  // Menu3




    static public int level; //��ü ���� ���� ���� !!!!!! (1�̸� easy/ 2�� normal/ 3�̸� hard)



    void Start()
    {
        //�ʱ� �޴� Ȱ��ȭ ����. 
        gameLabel1.SetActive(true);
        gameLabel2.SetActive(false);
        gameLabel3.SetActive(false);

    
        

    }

    // Update is called once per frame
    void Update()
    {
  
    }


    //�޴� 1 - Start1 ��ư
    public void OnclickL1Start1() 
    { 
          gameLabel1.SetActive(false);
          gameLabel2.SetActive(true);
    }

  




    //���̵� ���� ��ư �Լ�
    public void OnclickL1() 
    {
        //float _time = 90.0f;  // Easy
        level = 1;

        gameLabel2.SetActive(false);
        gameLabel3.SetActive(true);

        //Debug.Log(_time);

    }
    public void OnclickL2()
    {


        //float _time = 60.0f;  // Normal
        level = 2;

        gameLabel2.SetActive(false);
        gameLabel3.SetActive(true);

        //Debug.Log(_time);
    }
    public void OnclickL3()
    { 
        //float _time = 30.0f;  // Hard
        level = 3;

        gameLabel2.SetActive(false);
        gameLabel3.SetActive(true);

        //Debug.Log(_time);
    }









}
