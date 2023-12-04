using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class MovableToy : MonoBehaviour
    {
        Rigidbody rigid;
        private void Awake()
        {
            rigid = GetComponentInChildren<Rigidbody>();
        }
        /// <summary>
        /// �ӵ��� ������ �ʱⰪ ����
        /// </summary>
        /// <param name="v_speed"></param>
        /// <param name="v_size"></param>
        public void Init(float v_speed, float v_size) 
        { 
        }

        private void OnCollisionEnter(Collision collision)
        {
            // ���� ���丷
            rigid.mass /= 2;
        }
    }
}