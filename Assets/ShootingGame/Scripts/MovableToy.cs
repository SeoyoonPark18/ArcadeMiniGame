using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class MovableToy : MonoBehaviour
    {
        Rigidbody rigid;
        float speed;

        private void Awake()
        {
            rigid = GetComponentInChildren<Rigidbody>();
        }

        public void StartMove()
        {

        }

        public void SetSpeed(float v_speed)
        {
            speed = v_speed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("collision");
            if (collision.transform.CompareTag("Bullet"))
            {
                // 질량 반토막
                rigid.mass /= 2;
            }
            LevelManager.Instance.toyCollidesEvent.Invoke();    // 총알 모두 소모했을 시 게임 종료 트리거 갱신
        }
    }
}