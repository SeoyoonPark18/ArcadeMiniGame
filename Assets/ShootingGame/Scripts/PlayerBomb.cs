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
        // ��ȭ �Ѿ� ������Ʈ ������
        public GameObject buffedBombFactory;
        //��ô �Ŀ�
        public float bombPower = 15f;

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            //���콺 ���� ��ư �Է�
            if (Input.GetMouseButtonDown(0))
            {
                if(LevelManager.Instance.BULLET_COUNTS <= 0)
                {
                    return;
                }
                bool isBuffedBullet = LevelManager.Instance.UsePowerBullet();
                LevelManager.Instance.playerFiresEvent.Invoke();    // �÷��̾� �߻� �̺�Ʈ Ʈ����

                if (isBuffedBullet)     // ��ȭ �Ѿ�
                {
                    //�Ѿ� ����
                    GameObject bomb = Instantiate(buffedBombFactory);

                    //�Ѿ� ��ġ�� �߻� ��ġ�� �̵�
                    bomb.transform.position = bombPosition.transform.position;

                    //��ź�� ������ٵ� ������Ʈ�� ������
                    Rigidbody rb = bomb.GetComponent<Rigidbody>();

                    //������ٵ�κ��� ī�޶��� �������� ��ź �Ŀ� ��
                    rb.AddForce(Camera.main.transform.forward * bombPower * 1.5f, ForceMode.Impulse);
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
