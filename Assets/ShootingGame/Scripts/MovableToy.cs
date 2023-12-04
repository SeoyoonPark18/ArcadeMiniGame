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
        /// 속도와 사이즈 초기값 세팅
        /// </summary>
        /// <param name="v_speed"></param>
        /// <param name="v_size"></param>
        public void Init(float v_speed, float v_size) 
        { 
        }

        private void OnCollisionEnter(Collision collision)
        {
            // 질량 반토막
            rigid.mass /= 2;
        }
    }
}