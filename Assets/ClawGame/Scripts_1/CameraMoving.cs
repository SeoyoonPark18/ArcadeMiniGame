using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMoving : MonoBehaviour
{
    public Transform target;

    public float rotationSpeed = 5f; 
    public float minRotationAngle = -90f; 
    public float maxRotationAngle = 90f; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;                      
           
            float currentRotationAngle = transform.rotation.eulerAngles.y;
                      
            float newRotationAngle = currentRotationAngle + mouseX;

            newRotationAngle = Mathf.Clamp(newRotationAngle, currentRotationAngle + minRotationAngle, currentRotationAngle + maxRotationAngle);

            transform.RotateAround(target.position, Vector3.up, min_max(newRotationAngle - currentRotationAngle, gameObject.transform.rotation.eulerAngles.y));
           
            if (gameObject.transform.rotation.eulerAngles.y >= 50 && gameObject.transform.rotation.eulerAngles.y <=210)
            {               
                gameObject.transform.rotation = Quaternion.Euler(0, 50, 0);
            }  
            if (gameObject.transform.rotation.y >= 310)
                gameObject.transform.rotation = Quaternion.Euler(0, 310, 0);
        }
    }

    float min_max(float change,float cur)
    {        
        float result = 0;
        if(change>0)
        {
            if (cur + change > 50 && cur + change<=210)
                result = 50 - cur;
            else
                result = change;
        }
        else if(change < 0) 
        {
            if (cur + change <310 && cur + change>210)
                result = 310-cur;
            else
                result = change;
        }

        return result;
        
    }
}
    




 