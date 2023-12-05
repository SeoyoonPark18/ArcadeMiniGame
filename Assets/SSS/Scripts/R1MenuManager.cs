using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R1MenuManager : MonoBehaviour
{
    public int SP;

    public GameObject R1Menu1;  // Round1-LiveMenu
    public GameObject R1Menu2;  // Round1-DieMenu



    // Start is called before the first frame update
    void Start()
    {
        // ¸ñ¼û
        SP = TimeManager.Starpoint;

        R1Menu1.SetActive(false);
        R1Menu2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ¸ñ¼û = 0
        //if (SP == 0)
        //{
        //    Debug.Log("³Ê Á×À½");
        //    R1Menu2.SetActive(true);
        //}


        if (SP == 1)
        {
            Debug.Log("³Ê Á×À½1");
        }
        else if (SP == 2)
        {
            Debug.Log("³Ê Á×À½2");
        }
        else if (SP == 3)
        {
            Debug.Log("³Ê Á×À½3");
        }

    }
}
