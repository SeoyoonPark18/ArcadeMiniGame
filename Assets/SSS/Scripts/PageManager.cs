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

        // ����ȯ-Roun1
        /*
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("Round1");
        }
        */

        // ����ȯ-Roun2
        /*
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("Round1");
        }
        */

        Cursor.lockState = CursorLockMode.Confined;

    }


    public void OnclickScene()
    {
        SceneManager.LoadScene("Round1");

    }



}
