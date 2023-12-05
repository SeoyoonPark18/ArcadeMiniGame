using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{

    public class PowerBomb : MonoBehaviour
    {
        [SerializeField] private GameObject hitEffect;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.CompareTag("Toy"))
            {
                // ����Ʈ
                if (hitEffect != null)
                {
                    GameObject fx = Instantiate(hitEffect, collision.GetContact(0).point, hitEffect.transform.rotation);
                }

                // ��ȭ�Ѿ� ���ֱ�
                Rigidbody rigid = collision.transform.GetComponent<Rigidbody>();
                rigid.AddForce((collision.transform.position - transform.position) * 300, ForceMode.Impulse);
            }
        }
    }

}