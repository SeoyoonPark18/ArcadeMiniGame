using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollExit : MonoBehaviour
{
    static public bool _isSuccess = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        _isSuccess = true;
        Debug.Log("냐미");
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        _isSuccess = true;
        Debug.Log("냐미");
    }
}
