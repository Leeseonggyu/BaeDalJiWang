using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Move move;
    public float jump = 15;
    public float Wtime, jumpRot = 0, jumpTime = 0;
    [SerializeField] public bool canJump, jumpDown, jumpButton, jCount, hitBuilding, ButtonClick;
    [SerializeField] public Transform tr;
    RaycastHit Uhit, Dhit, Lhit, Rhit;

    // Start is called before the first frame update
    void Start()
    {
        Wtime = 0;
        canJump = true; jumpDown = false; jumpButton = false; jCount = false; hitBuilding = false; ButtonClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        Wtime += Time.deltaTime;
        
        if (tr.transform.position.y != 0.2)
        {//점프 중 Y축 위치 고정
            if (Wtime > 0.2 && !canJump)//점프 중 Y축 위치 고정 방지
            {
                tr.transform.position = new Vector3(tr.transform.position.x, 0.2f, transform.position.z);
                Wtime = 0;
            }
            else if (Wtime > 0.2 && !hitBuilding)//빌딩에 충돌하고 있지 않을때 0.2초마다 Y축 위치 고정
            {
                tr.transform.position = new Vector3(tr.transform.position.x, 0.2f, tr.transform.position.z);
                Wtime = 0;
            }
            else
                tr.transform.position = new Vector3(tr.transform.position.x, tr.transform.position.y, tr.transform.position.z);

        }
        var Pmove= GameObject.FindWithTag("Player").GetComponent<Move>();
        if (Input.GetKeyDown(KeyCode.Space) && !jumpButton)//점프
        {
            jumpTime = 0;
            jumpButton = true;
            Pmove.PlaySoundSp3();
        }
        else if (ButtonClick && !jumpButton)//점프
        {
            jumpTime = 0;
            jumpButton = true; ButtonClick = false;
            Pmove.PlaySoundSp3();
        }
        
    }
    private void FixedUpdate()
    {
        var cd = GameObject.Find("GameCanvas").transform.Find("Restart Count Down UI").transform.Find("Text").GetComponent<CountDown>();
        if (canJump && jumpButton && !cd.act)//점프가 가능한 상태고 점프 버튼을 눌렀으며 카운트 다운 중이 아닐때만 점프 & 회전 동작 실행
        {
            controlJump();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Building")
            hitBuilding = true;
        if (!collision.gameObject.transform.parent) { }
        else
        {
            if (collision.gameObject.transform.parent.gameObject.tag == "Building")//부모오브젝트가 존재할때 그 오브젝트 태그이름이 빌딩인 경우
                hitBuilding = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Building")
            hitBuilding = false;
        if (!collision.gameObject.transform.parent) { }
        else
        {
            if (collision.gameObject.transform.parent.gameObject.tag == "Building")//부모오브젝트가 존재할때 그 오브젝트 태그이름이 빌딩인 경우
                hitBuilding = false;
        }
    }

    void controlJump()
    {
        //바닥에 있을때 점프 버튼을 누른 후이며 한번 정상에 도달한 이후이므로 점프 버튼을 해제해서 함수 반복 호출 상태 조건 해제
        if (jumpTime < 0 && jumpButton && jCount)
            jumpButton = false;
        if (jumpTime >= jump)//정상 도달시 하강 시키고 카운트 한다.
            jumpDown = true; jCount = true;
        if (jumpTime <= 0)//바닥에 도달하면 더 이상 아래로 내려가지 않도록 한다.
            jumpDown = false;

        bool U_Ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out Uhit, 1f);
        bool D_Ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out Dhit, 1f);
        bool R_Ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out Rhit, 1f);
        bool L_Ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out Lhit, 1f);

        if (!jumpDown)//상승
        {
            if (U_Ray || D_Ray || R_Ray || L_Ray)
            {
                //Debug.Log("RayHit");
                bool HitBool = false;
                if (U_Ray) HitBool = Uhit.collider.gameObject.tag == "Building";
                if (D_Ray) HitBool = Dhit.collider.gameObject.tag == "Building";
                if (R_Ray) HitBool = Rhit.collider.gameObject.tag == "Building";
                if (L_Ray) HitBool = Lhit.collider.gameObject.tag == "Building";

                if (HitBool)
                {
                    //Debug.Log("Hit!");
                    //jumpRot = -90;
                    tr.transform.position = new Vector3(tr.transform.position.x, tr.transform.position.y, tr.transform.position.z);
                }
                else
                {
                    if (!hitBuilding)
                    {
                        jumpTime += 0.8f;
                        float temp = jumpTime / jump;//최대 높이 비례 점프 높이별 퍼센트 계산
                        jumpRot = 180 * temp;
                    }
                    else
                        jumpRot = 0;
                    tr.transform.position = new Vector3(tr.transform.position.x, 0.4f + jumpTime, tr.transform.position.z);
                }
            }
            else
            {
                if (!hitBuilding)
                {
                    //Debug.Log("ActROT");
                    jumpTime += 0.8f;
                    float temp = jumpTime / jump;//최대 높이 비례 점프 높이별 퍼센트 계산
                    jumpRot = 180 * temp;
                }
                else
                    jumpRot = 0;
                tr.transform.position = new Vector3(tr.transform.position.x, 0.4f + jumpTime, tr.transform.position.z);
            }
        }
        else//하강
        {
            if (U_Ray || D_Ray || R_Ray || L_Ray)
            {
                //Debug.Log("RayHit");
                bool HitBool = false;
                if (U_Ray)
                    HitBool = Uhit.collider.gameObject.tag == "Building";
                if (D_Ray)
                    HitBool = Dhit.collider.gameObject.tag == "Building";
                if (R_Ray)
                    HitBool = Rhit.collider.gameObject.tag == "Building";
                if (L_Ray)
                    HitBool = Lhit.collider.gameObject.tag == "Building";

                if (HitBool)
                {
                    //Debug.Log("Hit!");
                    jumpRot = 0;
                    tr.transform.position = new Vector3(tr.transform.position.x, tr.transform.position.y, tr.transform.position.z);
                }
                else
                {
                    if (!hitBuilding)
                    {
                        jumpTime -= 0.8f;
                        float temp = jumpTime / jump;
                        float temp2 = 1 - temp;
                        jumpRot = 180 * temp2 + 180;//감소해서 현재 높이가 낮을 수록 1에 가까워지게 퍼센트 계산
                    }
                    else
                        jumpRot = 0;
                    tr.transform.position = new Vector3(tr.transform.position.x, jumpTime, tr.transform.position.z);
                }
            }
            else
            {
                if (!hitBuilding)
                {
                    jumpTime -= 0.8f;
                    float temp = jumpTime / jump;
                    float temp2 = 1 - temp;
                    jumpRot = 180 * temp2 + 180;//감소해서 현재 높이가 낮을 수록 1에 가까워지게 퍼센트 계산
                }
                else
                    jumpRot = 0;
                tr.transform.position = new Vector3(tr.transform.position.x, 0.4f + jumpTime, tr.transform.position.z);
            }
        }
    }
}