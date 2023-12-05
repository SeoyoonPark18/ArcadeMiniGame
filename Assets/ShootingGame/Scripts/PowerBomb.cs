using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{

    public class PowerBomb : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.CompareTag("Toy"))
            {
                // TODO ����Ʈ
                Rigidbody rigid = collision.transform.GetComponent<Rigidbody>();
                rigid.AddForce((collision.transform.position - transform.position) * 300, ForceMode.Impulse);
            }
        }
    }

}