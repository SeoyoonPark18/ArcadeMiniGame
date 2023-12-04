using ShootingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerBomb
{
    public class PlayerBomb : MonoBehaviour
    {
        //�߻� ��ġ
        public GameObject bombPosition;
        // �Ѿ� ������Ʈ ������
        public GameObject bombFactory;
        //��ô �Ŀ�
        public float bombPower = 15f;
        //�Ѿ� ����Ʈ
        public GameObject bulletEffect;
        //��ƼŬ �ý���
        ParticleSystem ps;

        void Start()
        {
            ps = bulletEffect.GetComponent<ParticleSystem>(); //bulletEffect�� ������Ʈ�� ������
        }

        // Update is called once per frame
        void Update()
        {
            //���콺 ���� ��ư �Է�
            if (Input.GetMouseButtonDown(0))
            {
                LevelManager.Instance.playerFiresEvent.Invoke();    // �÷��̾� �߻� �̺�Ʈ Ʈ����
                bool isBuffedBullet = LevelManager.Instance.UsePowerBullet();

                if (isBuffedBullet)     // ��ȭ �Ѿ�
                {

                }
                else    // �Ϲ� �Ѿ�
                {
                    //�Ѿ� ����
                    GameObject bomb = Instantiate(bombFactory);

                    //�Ѿ� ��ġ�� �߻� ��ġ�� �̵�
                    bomb.transform.position = bombPosition.transform.position;

                    //��ź�� ������ٵ� ������Ʈ�� ������
                    Rigidbody rb = bomb.GetComponent<Rigidbody>();

                    //������ٵ�κ��� ī�޶��� �������� ��ź �Ŀ� ��
                    rb.AddForce(Camera.main.transform.forward * bombPower, ForceMode.Impulse);
                }
            }
        }
    }
}
