using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMove : MonoBehaviour
{
    enum ClawState{
        Idle,
        Move,
        Down,
        CloseClaw,
        Up,
        Return,
        OpenClaw
    }

    ClawState clawState;
    float moveSpeed = 2.5f; //이동 속도
    float downSpeed = 1.5f; //하강 속도
    float downTime = 1.5f; //하강 시간
    public GameObject claw_1; //집게 오브젝트
    public GameObject claw_2;
    public GameObject claw_3;
    public GameObject claw_4;
    float gravingSpeed = 0.2f; //잡는 속도
    float gravingTime = 0.6f; //잡는 시간
    Vector3 originPos;


    void Start()
    {
        clawState = ClawState.Idle;
        originPos = transform.position; //오브젝트 초기 위치 값
    }

    void Update()
    {
        switch(clawState){
            case ClawState.Idle:
                Idle();
                break;
            case ClawState.Move:
                Move();
                break;
            case ClawState.Down:
                Down();
                break;
            case ClawState.CloseClaw:
                CloseClaw();
                break;           
            case ClawState.Up:
                Up();
                break;
            case ClawState.Return:
                Return();
                break;
            case ClawState.OpenClaw:
                OpenClaw();
                break;
        }
    }

    void Idle(){
        clawState = ClawState.Move;
    }
    void Move(){
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v); //이동 방향
        dir = dir.normalized;
        transform.position += dir * moveSpeed * Time.deltaTime; //오브젝트 이동

        if(Input.GetKeyDown(KeyCode.Space) == true){
            clawState = ClawState.Down;
            StartCoroutine(ClawDownStop());
        }
    }
    void Down(){
        Vector3 dir = new Vector3(0, -1f, 0); //방향(하강)
        dir = dir.normalized; 
        transform.position += dir * downSpeed * Time.deltaTime; //오브젝트 하강 
    }
    void CloseClaw(){
        claw_1.transform.Rotate(new Vector3(-1f,0,0) , gravingSpeed, Space.World); //잡는 동작
        claw_2.transform.Rotate(new Vector3(1f,0,0) , gravingSpeed, Space.World);
        claw_3.transform.Rotate(new Vector3(0,0,1f) , gravingSpeed, Space.World);
        claw_4.transform.Rotate(new Vector3(0,0,-1f) , gravingSpeed, Space.World);
    } 
    void Up(){
        Vector3 dir = new Vector3(0, 1f, 0); //방향(상승)
        dir = dir.normalized;
        transform.position += dir * downSpeed * Time.deltaTime; //오브젝트 상승
    }
    void Return(){
        if(Vector3.Distance(transform.position, originPos) > 0.1f){
            Vector3 dir = (originPos - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime; //원위치로 복귀
        }
        else{
            transform.position = originPos;
            clawState = ClawState.OpenClaw;  
            StartCoroutine(ClawLay()); 
        }
    }
    void OpenClaw(){
        claw_1.transform.Rotate(new Vector3(1f,0,0) , gravingSpeed, Space.World); //놓는 동작
        claw_2.transform.Rotate(new Vector3(-1f,0,0) , gravingSpeed, Space.World);
        claw_3.transform.Rotate(new Vector3(0,0,-1f) , gravingSpeed, Space.World);
        claw_4.transform.Rotate(new Vector3(0,0,1f) , gravingSpeed, Space.World);
    }

    IEnumerator ClawDownStop(){
        yield return new WaitForSeconds(downTime); //오브젝트 1초 하강 후 하강 멈춤
        clawState = ClawState.CloseClaw;
        StopAllCoroutines();
        StartCoroutine(ClawGrab());
    }
    IEnumerator ClawUpStop(){
        yield return new WaitForSeconds(downTime); //오브젝트 1초 하강 후 하강 멈춤
        clawState = ClawState.Return;
    }

    IEnumerator ClawGrab(){
        yield return new WaitForSeconds(gravingTime); //n초간 오므린 후 잡기 멈춤
        clawState = ClawState.Up;
        StopAllCoroutines();
        StartCoroutine(ClawUpStop());
    }
    IEnumerator ClawLay(){
        yield return new WaitForSeconds(gravingTime); //n초간 놓기
        clawState = ClawState.Idle;
        StopAllCoroutines();
    }

}
