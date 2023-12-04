using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CamRotate
{
    public class CamRotate : MonoBehaviour
    {
        //마우스 회전 속도
        public float rotSpeed = 4.0f;

        //회전 값 변수
        private float mx = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Cursor.lockState = CursorLockMode.Confined;
            //컨트롤 + p로 플레이시작하기

            //좌우로 움직인 마우스 이동량 * 속력
            float yRot = Input.GetAxis("Mouse X")*rotSpeed;
            //현재 y축 회전값에 더한 새로운 회전각도 계산
            float my = transform.eulerAngles.y + yRot;

            //위아래로 움직인 마우스의 이동량 * 속도
            float xRot = -Input.GetAxis("Mouse Y") * rotSpeed;

            //상하 회전을 -45~ 80도로 제한
            mx = Mathf.Clamp(mx+xRot, -45f, 80f); //수학관련함수: Mathf. Clapm(최소 최대값 설정해서 float값이 범이 이외의 값을 넘지 않도록)

            //카메라 회전
            transform.eulerAngles = new Vector3(mx, my, 0); //회전시키는 함수에 축 값을 넣어줌
                                                             //eulerAngles 값은 360도를 넘어가면 계산이 실패하기 때문에 현재시간을 곱해주면 안 됨
        }
    }
}
