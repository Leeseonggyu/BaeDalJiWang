using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Swipe : MonoBehaviour
{
    public GameManager gm;
    public bool CountAgain = false;
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;
    private Touch tempTouch;
    private Vector3 touchPos;

    private void Update()
    {
        var Pmove = GameObject.FindWithTag("Player").GetComponent<Move>();
        var Pjump = GameObject.FindWithTag("Player").GetComponent<Jump>();
        var Pcoll = GameObject.FindWithTag("Player").GetComponent<PlayerCollisionManager>();
        var cd = GameObject.Find("GameCanvas").transform.Find("Restart Count Down UI").transform.Find("Text").GetComponent<CountDown>();

        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        if(!gm.act)//정지중에는 입력받지 않음
        {
#if UNITY_EDITOR
            #region Stabdalone Inputs   
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Pmove.moveDir = Direction.Stop;//UI 클릭시 동시동작 방지
                    //Debug.Log("Stop");
                }
                tap = true;
                isDraging = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                //Pmove.ManageSpeed();
                isDraging = false;
                Reset();
            }
            #endregion   
#endif
            #region Mobile Inputs
            if (Input.GetMouseButtonDown(0))
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    {
                        Pmove.moveDir = Direction.Stop;//UI 클릭시 동시동작 방지
                        //Debug.Log("Stop");
                    }
                }
                
            }
            if (isDraging)
                Pmove.canCount = false;//터치 유지중에는 이동 정지 상태 따라서 거리 카운트도 정지
            else if (CountAgain && !Pcoll.hitOther && !cd.act)//다른 물체와 충돌 및 카운트 다운 중에는 거리카운트를 재개시키지 않는다.
                Pmove.canCount = true;
            
            if (Input.touches.Length > 0.2f)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    tap = true;
                    isDraging = true;
                    startTouch = Input.mousePosition;
                    
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                        Pmove.ManageSpeed();
                    isDraging = false;
                    Reset();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Reset();
            }
            #endregion
        }


        //Calculate the distance
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        //Is Cross DeadZone?
        if (swipeDelta.magnitude > 125)
        {
            //방향
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                Pmove.rb.constraints = RigidbodyConstraints.FreezePositionZ;
                Pmove.moveHorizontal = true;
                //L(W) or R(E)
                if (x < 0)
                {
                    swipeLeft= true;
                    Pmove.moveDir = Direction.W;
                }
                else
                {
                    swipeRight = true;
                    Pmove.moveDir = Direction.E;
                }
            }
            else
            {
                Pmove.rb.constraints = RigidbodyConstraints.FreezePositionX;
                Pmove.moveHorizontal = false;
                //U(N) or D(S)
                if (y < 0)
                {
                    swipeDown = true;
                    Pmove.moveDir = Direction.S;
                }
                else
                {
                    swipeUp = true;
                    Pmove.moveDir = Direction.N;
                }
            }

            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    public bool Tap { get { return tap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
}
