using ShootingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{

    public class BulletItem : MonoBehaviour
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

                // 아이템 추가
                LevelManager.Instance.AddPowerBullet();
                Destroy(this.gameObject);
            }
        }
    }

}