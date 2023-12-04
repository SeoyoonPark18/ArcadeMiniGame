using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CamRotate
{
    public class CamRotate : MonoBehaviour
    {
        //���콺 ȸ�� �ӵ�
        public float rotSpeed = 4.0f;

        //ȸ�� �� ����
        private float mx = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Cursor.lockState = CursorLockMode.Confined;
            //��Ʈ�� + p�� �÷��̽����ϱ�

            //�¿�� ������ ���콺 �̵��� * �ӷ�
            float yRot = Input.GetAxis("Mouse X")*rotSpeed;
            //���� y�� ȸ������ ���� ���ο� ȸ������ ���
            float my = transform.eulerAngles.y + yRot;

            //���Ʒ��� ������ ���콺�� �̵��� * �ӵ�
            float xRot = -Input.GetAxis("Mouse Y") * rotSpeed;

            //���� ȸ���� -45~ 80���� ����
            mx = Mathf.Clamp(mx+xRot, -45f, 80f); //���а����Լ�: Mathf. Clapm(�ּ� �ִ밪 �����ؼ� float���� ���� �̿��� ���� ���� �ʵ���)

            //ī�޶� ȸ��
            transform.eulerAngles = new Vector3(mx, my, 0); //ȸ����Ű�� �Լ��� �� ���� �־���
                                                             //eulerAngles ���� 360���� �Ѿ�� ����� �����ϱ� ������ ����ð��� �����ָ� �� ��
        }
    }
}
