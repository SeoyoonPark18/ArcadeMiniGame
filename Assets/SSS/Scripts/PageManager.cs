using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Cursor.lockState = CursorLockMode.Confined;

    }


    public void OnclickScene()
    {
        SceneManager.LoadScene("Round1");

    }

    public void OnclickScene2()
    {
        SceneManager.LoadScene("Round2");

    }



}
