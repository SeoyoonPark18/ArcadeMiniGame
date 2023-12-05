using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{

    public class PooItem : MonoBehaviour
    {
        public GameObject hitEffect;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Bullet"))
            {
                if (hitEffect != null)
                {
                    GameObject fx = Instantiate(hitEffect, transform.position, transform.rotation);
                }

                // 방해 아이템 사용
                LevelManager.Instance.AtePoo();
                Destroy(this.gameObject);
            }
        }
    }

}