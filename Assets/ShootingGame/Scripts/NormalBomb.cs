using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class NormalBomb : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Toy"))
            {
                // TODO ¿Ã∆Â∆Æ
            }
        }
    }
}