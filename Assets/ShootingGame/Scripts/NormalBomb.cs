using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class NormalBomb : MonoBehaviour
    {
        [SerializeField] private GameObject hitEffect;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Toy"))
            {
                // ����Ʈ
                if (hitEffect != null)
                {
                    GameObject fx = Instantiate(hitEffect, collision.GetContact(0).point, hitEffect.transform.rotation);
                }
            }
        }
    }
}