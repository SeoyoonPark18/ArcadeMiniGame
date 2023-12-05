using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    

    // UI 오브젝트 변수
    public GameObject gameLabel1;  // Menu1
    public GameObject gameLabel2;  // Menu2
    public GameObject gameLabel3;  // Menu3




    static public int level; //전체 레벨 지정 변수 !!!!!! (1이면 easy/ 2면 normal/ 3이면 hard)



    void Start()
    {
        //초기 메뉴 활성화 설정. 
        gameLabel1.SetActive(true);
        gameLabel2.SetActive(false);
        gameLabel3.SetActive(false);

    
        

    }

    // Update is called once per frame
    void Update()
    {
  
    }


    //메뉴 1 - Start1 버튼
    public void OnclickL1Start1() 
    { 
          gameLabel1.SetActive(false);
          gameLabel2.SetActive(true);
    }

  




    //난이도 설정 버튼 함수
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
