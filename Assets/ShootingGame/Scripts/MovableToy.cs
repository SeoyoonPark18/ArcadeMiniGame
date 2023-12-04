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
            if (collision.transform.CompareTag("Bullet"))
            {
                StartCoroutine(MassDiscountCo());
            }
            LevelManager.Instance.toyCollidesEvent.Invoke();    // �Ѿ� ��� �Ҹ����� �� ���� ���� Ʈ���� ����
        }

        IEnumerator MassDiscountCo()
        {
            yield return new WaitForSeconds(1f);
            // ���� ���丷
            rigid.mass /= 2;
        }
    }
}