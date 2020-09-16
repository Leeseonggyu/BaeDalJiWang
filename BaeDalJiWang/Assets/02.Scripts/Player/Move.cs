using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum Direction
{
    N, S, E, W, Stop
}

public class Move : MonoBehaviour
{
    public float Speed = 0;
    public GameManager gm;
    public Rigidbody rb;
    public Vector3 pos;
    public Direction moveDir;
    public int SpeedLevel;
    public Swipe swipeControls;
    private Vector3 desiredPosition;
    public float countDistance, JumpDistance;

    public int m_SpeedSoundCount = 2;
    AudioSource[] m_AudioSource = null;

    public AudioClip m_Audio_Sp2 = null;
    public AudioClip m_Audio_Sp3 = null;

    public Transform tr;
    [SerializeField] public bool moveHorizontal, canCount, maxSpeed;
    void PlayOneShot(AudioClip pAudioClip)
    {

        for (int i = 0; i < m_AudioSource.Length; i++)
        {
            if (m_AudioSource[i].isPlaying == false)
            {
                m_AudioSource[i].PlayOneShot(pAudioClip);
                break;
            }
        }
    }
    public void PlaySoundSp2()
    {
        PlayOneShot(m_Audio_Sp2);
    }
    public void PlaySoundSp3()
    {
        PlayOneShot(m_Audio_Sp3);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (m_AudioSource == null)
        {
            m_AudioSource = new AudioSource[m_SpeedSoundCount];

            for (int i = 0; i < m_AudioSource.Length; i++)
            {
                AudioSource pAudioSource = this.gameObject.AddComponent<AudioSource>();
                pAudioSource.rolloffMode = AudioRolloffMode.Linear;
                m_AudioSource[i] = pAudioSource;
            }
        }
        SpeedLevel = 0; countDistance = 0; JumpDistance = 15000;
        canCount = false;
        //스크립트가 실행된 후 처음 수행되는 Start 함수에서 Rigidbody 컴포넌트를 할당
        rb = GetComponent<Rigidbody>();
        //스크립트가 실행된 후 처음 수행되는 Start 함수에서 Transform 컴포넌트를 할당
        tr = GetComponent<Transform>();
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        moveDir = Direction.E;
    }

    public void ManageSpeed()
    {
        //Debug.Log("MS");
        if (moveDir == Direction.Stop)
        {
            if (GameObject.Find("GameCanvas").transform.Find("SpeedLevel").transform.Find("SpeedLevel1").gameObject.activeSelf == true)
                GameObject.Find("GameCanvas").transform.Find("SpeedLevel").transform.Find("SpeedLevel1").gameObject.SetActive(false);
            if (GameObject.Find("GameCanvas").transform.Find("SpeedLevel").transform.Find("SpeedLevel2").gameObject.activeSelf == true)
                GameObject.Find("GameCanvas").transform.Find("SpeedLevel").transform.Find("SpeedLevel2").gameObject.SetActive(false);
            GameObject.Find("GameCanvas").transform.Find("SpeedLevel").transform.Find("SpeedLevel0").gameObject.SetActive(true);
            Speed = 0; SpeedLevel = 0; maxSpeed = false;
            rb.velocity = new Vector3(0, 0, 0);
        }
        else if (SpeedLevel == 0)
        {
            if (GameObject.Find("GameCanvas").transform.Find("SpeedLevel").transform.Find("SpeedLevel2").gameObject.activeSelf == true)
                GameObject.Find("GameCanvas").transform.Find("SpeedLevel").transform.Find("SpeedLevel2").gameObject.SetActive(false);
            GameObject.Find("GameCanvas").transform.Find("SpeedLevel").transform.Find("SpeedLevel0").gameObject.SetActive(false);
            GameObject.Find("GameCanvas").transform.Find("SpeedLevel").transform.Find("SpeedLevel1").gameObject.SetActive(true);
            Speed = 1500; SpeedLevel++; maxSpeed = false;
            PlaySoundSp2();
        }
        else if (SpeedLevel == 1)
        {
            GameObject.Find("GameCanvas").transform.Find("SpeedLevel").transform.Find("SpeedLevel1").gameObject.SetActive(false);
            GameObject.Find("GameCanvas").transform.Find("SpeedLevel").transform.Find("SpeedLevel2").gameObject.SetActive(true);
            Speed = 3000; SpeedLevel++; maxSpeed = true;
            PlaySoundSp3();
        }
        else if (SpeedLevel == 2)
        {
            maxSpeed = true;
        }
        else
            Debug.Log("스피드 에러");
    }

    private void FixedUpdate()
    {
        float h = gm.act ? 0 : Input.GetAxisRaw("Horizontal");
        float v = gm.act ? 0 : Input.GetAxisRaw("Vertical");

        //float h = gm.act ? 0 : Input.GetAxisRaw("Mouse X");
        //float v = gm.act ? 0 : Input.GetAxisRaw("Mouse Y");

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {//버튼을 누를때마다 가속
            ManageSpeed();//중복 동작 방지
        }

        if (h != 0)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            moveHorizontal = true;

            if (h > 0)
            {
                moveDir = Direction.E;//1
            }
            else
            {
                moveDir = Direction.W;//-1
            }
        }
        if (v != 0)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            moveHorizontal = false;

            if (v > 0)
            {
                moveDir = Direction.N;//1
            }
            else
            {
                moveDir = Direction.S;//-1
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))//엔터키 정지
        {
            moveDir = Direction.Stop;
            gm.Action(); ManageSpeed(); gm.Action();
        }

        if (canCount)
        {
            //1프레임당 캐릭터 이동 거리 = 캐릭터 속도* Time.deltaTime
            if (countDistance <= JumpDistance)
            {
                countDistance += Speed * Time.deltaTime;
            }
            else
            {
                GameObject.Find("GameCanvas").transform.Find("JumpButton").gameObject.SetActive(true);
            }
        }

        //캐릭터 이동 & 점프중 회전 연동
        var Pjump = GameObject.FindWithTag("Player").GetComponent<Jump>();
        var cd = GameObject.Find("GameCanvas").transform.Find("Restart Count Down UI").transform.Find("Text").GetComponent<CountDown>();
        if (tr.transform.position.y > Pjump.jump)//점프 높이 이상 이동 방지
            tr.transform.position = new Vector3(tr.transform.position.x, Pjump.jump - 0.1f, tr.transform.position.z);

        if (!cd.act && tr.transform.position.y < Pjump.jump + 5)//카운트 다운 동안 이동 방지 
        {
            switch (moveDir)
            {
                case Direction.N:
                    rb.velocity = new Vector3(0, 0, Speed * Time.fixedDeltaTime);
                    if (Pjump.jumpButton)
                        rotDirection(moveDir);
                    else
                        tr.transform.rotation = Quaternion.Euler(0, 0, 0);//위로 회전
                    break;
                case Direction.S:
                    rb.velocity = new Vector3(0, 0, -Speed * Time.fixedDeltaTime);
                    if (Pjump.jumpButton)
                        rotDirection(moveDir);
                    else
                        tr.transform.rotation = Quaternion.Euler(0, 180, 0);//아래로 회전
                    break;
                case Direction.E:
                    rb.velocity = new Vector3(Speed * Time.fixedDeltaTime, 0, 0);
                    if (Pjump.jumpButton)
                        rotDirection(moveDir);
                    else
                        tr.transform.rotation = Quaternion.Euler(0, 90, 0);//오른쪽 회전
                    break;
                case Direction.W:
                    rb.velocity = new Vector3(-Speed * Time.fixedDeltaTime, 0, 0);
                    if (Pjump.jumpButton)
                        rotDirection(moveDir);
                    else
                        tr.transform.rotation = Quaternion.Euler(0, -90, 0);//왼쪽 회전
                    break;
                case Direction.Stop:
                    rb.velocity = new Vector3(0, 0, 0);
                    //Speed = 0;
                    break;
            }
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    void rotDirection(Direction RD)
    {//이동 방향에 따라 점프 중 회전방향 컨트롤
        var Pjump = GameObject.FindWithTag("Player").GetComponent<Jump>();
        if (RD == Direction.N)
        {
            rb.transform.rotation = Quaternion.Euler(Pjump.jumpRot, 0, 0);
        }
        else if (RD == Direction.S)
        {
            rb.transform.rotation = Quaternion.Euler(Pjump.jumpRot, 180, 0);
        }
        else if (RD == Direction.E)
        {
            rb.transform.rotation = Quaternion.Euler(Pjump.jumpRot, 90, 0);
        }
        else if (RD == Direction.W)
        {
            rb.transform.rotation = Quaternion.Euler(Pjump.jumpRot, -90, 0);
        }
            
    }
}