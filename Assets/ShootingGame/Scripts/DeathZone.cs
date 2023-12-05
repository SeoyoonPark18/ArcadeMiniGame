using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
    private void OnCollisionExit(Collision collision)
    {
        Destroy(collision.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
