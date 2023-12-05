using ShootingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Toy"))
            LevelManager.Instance.AddToy();
        Destroy(collision.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Toy"))
            LevelManager.Instance.AddToy();
        Destroy(other.gameObject);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Toy"))
            LevelManager.Instance.AddToy();
        Destroy(collision.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Toy"))
            LevelManager.Instance.AddToy();
        Destroy(other.gameObject);
    }
}
