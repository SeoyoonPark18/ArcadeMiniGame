using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CamRotate
{
    public class CamRotate : MonoBehaviour
    {
        //���콺 ȸ�� �ӵ�
        public float rotSpeed = 0.5f;

        //ȸ�� �� ����
        private float mx = 0;

        // ���� ȸ�� ���� ����
        private float horizontalLimit = 45f;

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
            // �¿� ���� �׽�Ʈ
            if(my > 180 && my < 360 && my < 360 - horizontalLimit)
                my = 360 - horizontalLimit;
            else if(my < 180 && my > 0 && my > horizontalLimit)
                my = horizontalLimit;

            //���Ʒ��� ������ ���콺�� �̵��� * �ӵ�
            float xRot = -Input.GetAxis("Mouse Y") * rotSpeed;

            //���� ȸ���� -45~ 80���� ����
            mx = Mathf.Clamp(mx + xRot, -45f, 80f); //���а����Լ�: Mathf.Clamp(�ּ� �ִ밪 �����ؼ� float���� ���� �̿��� ���� ���� �ʵ���)


            //ī�޶� ȸ��
            transform.eulerAngles = new Vector3(mx, my, 0); //ȸ����Ű�� �Լ��� �� ���� �־���
                                                             //eulerAngles ���� 360���� �Ѿ�� ����� �����ϱ� ������ ����ð��� �����ָ� �� ��
        }
    }
}
