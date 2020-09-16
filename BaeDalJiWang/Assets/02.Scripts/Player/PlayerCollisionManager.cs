using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0414

public class PlayerCollisionManager : MonoBehaviour
{
    public GameObject ring;
    public GameManager gm;
    public bool hitOther;
    Move player;
    [SerializeField] LayerMask obstacle;
    Rigidbody rb;

    private float LimitTime;      // 퀘스트 지역에서 몇 초 정차했는지 확인하는 시간
    float QuestTime = 0;  // 퀘스트 지역에서 0.2초 동안 정차해 있어야 퀘스트 수락 // public 으로 만들어 컴포넌트에서 관리 가능하게 하는 것 고려
    bool CanCount;
    public bool QuestSet;        // 퀘스트 진행 중인지 확인하는 값
    private bool SpawnSet;
    bool TriggerStay;

    // Start is called before the first frame update
    void Start()
    {
        hitOther = false; TriggerStay = false;
        CanCount = true;
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Move>();
    }

    void BuildingTag(GameObject other)
    {
        if (other.tag == "Building")
        {
            ObjData objData = other.GetComponent<ObjData>();
            objData.RingSpawnCount = true;

            var QP = GameObject.Find("QuestPointer").GetComponent<QuestPointer>();
            var QM = GameObject.Find("QuestManager").GetComponent<QuestManager>();
            
            if (QP.reach == true)
            {
                //Debug.Log("목적지");
                GameObject.Find("GameCanvas").transform.Find("QuestSuccessUI").gameObject.SetActive(true);
                if (QM.Level == "Easy")
                {
                    QM.QuestLevel.text = QM.Level;
                    QM.QuestReward.text = QM.questList[QM.questId].Reward[0].ToString();
                    
                }
                else if (QM.Level == "Normal")
                {
                    QM.QuestLevel.text = QM.Level;
                    QM.QuestReward.text = QM.questList[QM.questId].Reward[1].ToString();
                }
                else//Hard
                {
                    QM.QuestLevel.text = QM.Level;
                    QM.QuestReward.text = QM.questList[QM.questId].Reward[2].ToString();
                }
                QM.StoreTypeG.text = QM.questList[QM.questId].StoreType;
                float reward = QP.time * 100f;
                if (reward <= 0)
                    reward = 0;
                QM.TimeReward.text = ((int)(reward)).ToString();//실수를 정수형변환 후 string화 
                QM.TortalReward.text= (int.Parse(QM.QuestReward.text) + int.Parse(QM.TimeReward.text)).ToString();
                if (QP.time > 0)
                    QM.MsgboxS.text = QM.questList[QM.questId].MsgboxG;
                else
                    QM.MsgboxS.text = QM.questList[QM.questId].MsgboxLate;
            }
            else
            {
                //Debug.Log("출발지");
                GameObject.Find("GameCanvas").transform.Find("QuestMainUI").gameObject.SetActive(true);
                float distance = Mathf.Abs(Vector3.Distance(QM.QuestStart.gameObject.transform.position, QM.QuestGoal.gameObject.transform.position));//거리 계산(절대값)
                if (distance <= 100f)
                {
                    QM.Level = "Easy";
                    QM.QuestTime = QuestData.TimeLimit[0];
                    QM.RewardM.text = QM.questList[QM.questId].Reward[0].ToString();
                }
                else if (distance <= 300)
                {
                    QM.Level = "Normal";
                    QM.QuestTime = QuestData.TimeLimit[1];
                    QM.RewardM.text = QM.questList[QM.questId].Reward[1].ToString();
                }
                else
                {
                    QM.Level = "Hard";
                    QM.QuestTime = QuestData.TimeLimit[2];
                    QM.RewardM.text = QM.questList[QM.questId].Reward[2].ToString();
                }
                QM.DifficultyM.text = QM.Level;
                //Debug.Log(QM.Level);
                QM.TimeLimitM.text = string.Format("{0:N0}", QM.QuestTime.ToString());
                //Debug.Log(QM.TimeLimitM.text);
                
                QM.QuestNameM.text = QM.questList[QM.questId].questName;
                QM.StoreTypeM.text= QM.questList[QM.questId].StoreType;
                QM.MsgboxM.text = QM.questList[QM.questId].MsgboxM;
            }
            
            //Debug.Log(other.gameObject.name);
        }
    }

    void SwitchDir(GameObject other)
    {
        RaycastHit hit;
        switch (player.moveDir)//빌딩 태그 가진 콜리더에 부딪혔을때 레이캐스트 실행 빌딩이면 스크립트 출력
        {
            case Direction.N:
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 100.0f))
                {
                    //Debug.Log("N");
                    //BuildingTag(other);
                }
                break;
            case Direction.S:
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 100.0f))
                {
                    //Debug.Log("S");
                    //BuildingTag(other);
                }
                break;
            case Direction.E:
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 100.0f))
                {
                    //Debug.Log("E");
                    //BuildingTag(other);
                }
                break;
            case Direction.W:
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 100.0f))
                {
                    //Debug.Log("W")
                    //BuildingTag(other);
                }
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        var Pmove = GameObject.FindWithTag("Player").GetComponent<Move>();
        var Pjump = GameObject.FindWithTag("Player").GetComponent<Jump>();
        var PSwipe = GameObject.FindWithTag("Player").GetComponent<Swipe>();

        Pmove.canCount = false;
        PSwipe.CountAgain = false;
        if (!other.gameObject.transform.parent)
        {
            SwitchDir(other.gameObject);
            //Debug.Log(other.gameObject.name); 
        }
        else
        {
            SwitchDir(other.gameObject.transform.parent.gameObject);
            //Debug.Log(other.gameObject.transform.parent.gameObject.name);
        }

        if (other.gameObject.tag == "Ground")
            Pjump.canJump = true;
        else
            hitOther = true;
    }

    private void OnCollisionStay(Collision other)
    {
        var Pmove = GameObject.FindWithTag("Player").GetComponent<Move>();

        if (other.gameObject.tag == "Water")//물에 닿아있을때는 스피드 0단계로 계속 감속 Move스크립트에서 자동으로 스피드 레벨이 올라가 1단계로 고정
        {
            Pmove.SpeedLevel = 0;
            Pmove.ManageSpeed();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        var Pmove = GameObject.FindWithTag("Player").GetComponent<Move>();
        var PSwipe = GameObject.FindWithTag("Player").GetComponent<Swipe>();
        var cd = GameObject.Find("GameCanvas").transform.Find("Restart Count Down UI").transform.Find("Text").GetComponent<CountDown>();

        if (!cd.act)
        {
            Pmove.canCount = true;
            PSwipe.CountAgain = true;

            if (other.gameObject.tag != "Ground")
                hitOther = false;

            if (other.gameObject.tag == "Building")
            {
                //Pmove.rb.velocity = new Vector3(0, 0, 0);
                //Pmove.Speed = 0.0f; Pmove.SpeedLevel = 0;
                Pmove.canCount = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "QuestCollider")  // 플레이어 충돌 확인 // 퀘스트 진행 중인지 확인
        {
            var QP = GameObject.Find("QuestPointer").GetComponent<QuestPointer>();
            var QM = GameObject.Find("QuestManager").GetComponent<QuestManager>();
            //Debug.Log(QP.target.parent.name + " " + other.transform.parent.name);
            if (QP.target.parent == other.transform.parent)
            {
                var Pmove = GameObject.FindWithTag("Player").GetComponent<Move>();
                QP.CanCount = false;
                if (QuestSet == false)
                {
                    QuestRingSpawn(other);
                }
                else if(QuestSet == true)
                {
                    QuestRingSpawn(other);
                }
            }
        }
        TriggerStay = true;
    }
    private void QuestRingSpawn(Collider other)
    {
        var Pmove = GameObject.FindWithTag("Player").GetComponent<Move>();

        //Debug.Log("퀘스트 콜리더");
        LimitTime += Time.deltaTime;  // 정차 시간

        if (QuestTime < LimitTime)
        {
            Pmove.moveDir = Direction.Stop;
            ObjData objData = other.gameObject.transform.parent.gameObject.GetComponent<ObjData>();
            var tempObject = ring;

            if (!objData.RingSpawnCount)
            {
                Transform tempTr = other.GetComponent<Transform>();
                tempTr.position = new Vector3(other.transform.position.x, 0.1f, other.transform.position.z);
                tempObject = Instantiate(ring, tempTr.position, Quaternion.Euler(-90, 0, 0));
                tempObject.transform.parent = tempTr.gameObject.transform.parent;

                SpawnSet = true;
                objData.RingSpawnCount = true;
            }
            //Debug.Log(other.gameObject.transform.parent.gameObject);
            BuildingTag(other.gameObject.transform.parent.gameObject);
            //Debug.Log("멈춤");
            //questManager.QuestCheck(false);
            //QuestSet = true;            // 퀘스트 진행 중
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "QuestCollider")
        {
            var QP = GameObject.Find("QuestPointer").GetComponent<QuestPointer>();
            
            if (GameObject.Find("GameCanvas").transform.Find("QuestMainUI").gameObject.activeSelf == true)
                GameObject.Find("GameCanvas").transform.Find("QuestMainUI").gameObject.SetActive(false);
            if (GameObject.Find("GameCanvas").transform.Find("QuestSuccessUI").gameObject.activeSelf == true)
                GameObject.Find("GameCanvas").transform.Find("QuestSuccessUI").gameObject.SetActive(false);
            
            if (QP.target.parent.name == other.transform.parent.name)
            {
                QP.CanCount = true;
                SpawnSet = false;
                LimitTime = 0;
                
            }
            TriggerStay = false;
        }
    }
}
