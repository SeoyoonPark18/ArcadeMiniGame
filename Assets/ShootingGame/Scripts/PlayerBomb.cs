using ShootingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerBomb
{
    public class PlayerBomb : MonoBehaviour
    {
        //발사 위치
        public GameObject bombPosition;
        // 총알 오브젝트 프리팹
        public GameObject bombFactory;
        //투척 파워
        public float bombPower = 15f;
        //총알 이펙트
        public GameObject bulletEffect;
        //파티클 시스템
        ParticleSystem ps;

        void Start()
        {
            ps = bulletEffect.GetComponent<ParticleSystem>(); //bulletEffect의 컴포넌트를 가져옴
        }

        // Update is called once per frame
        void Update()
        {
            //마우스 왼쪽 버튼 입력
            if (Input.GetMouseButtonDown(0))
            {
                LevelManager.Instance.playerFiresEvent.Invoke();    // 플레이어 발사 이벤트 트리거
                bool isBuffedBullet = LevelManager.Instance.UsePowerBullet();

                if (isBuffedBullet)     // 강화 총알
                {

                }
                else    // 일반 총알
                {
                    //총알 생성
                    GameObject bomb = Instantiate(bombFactory);

                    //총알 위치를 발사 위치로 이동
                    bomb.transform.position = bombPosition.transform.position;

                    //폭탄의 리지드바디 컴포넌트를 가져옴
                    Rigidbody rb = bomb.GetComponent<Rigidbody>();

                    //리지드바디로부터 카메라의 정면으로 폭탄 파워 줌
                    rb.AddForce(Camera.main.transform.forward * bombPower, ForceMode.Impulse);
                }
            }
        }
    }
}
