using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class QuestManager : MonoBehaviour
{
    public int questId;//진행중인 퀘스트 ID
    public int EClear, NClear, HClear;//클리어한 난이도 별 퀘스트 수
    //public int questActionIndex;
    public ObjData QuestStart, QuestGoal;
    public bool GeneratedData, GenerateEnd;
    public Dictionary<int, QuestData> questList;
    public string Level;
    public Text DifficultyM, TimeLimitM, RewardM, QuestNameM, StoreTypeM;//Main
    public Text QuestLevel, QuestReward, TimeReward, TortalReward, StoreTypeG;//Start
    public Text MsgboxM, MsgboxS;
    public float QuestTime;
    public int totalClear;
    public Transform QSCol;
    public Transform QGCol;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GeneratedData = true;
    }
    public void SetUpStart()
    {
        EClear = 0; NClear = 0; HClear = 0;
        totalClear = 0;
        //Debug.Log("QQ");
        GeneratedData = false;
        var PS = GameObject.Find("PsManager").GetComponent<PScriptsManager>();
        int temp = Random.Range(0, PS.chObjData[0].Length);
        int temp2 = Random.Range(0, PS.chObjData[1].Length);
        QuestStart = PS.chObjData[0][temp];
        QuestGoal = PS.chObjData[1][temp2];
        //Debug.Log(QuestStart);
        //Debug.Log(QuestGoal);
        QSCol = QuestStart.transform.Find("QuestCollider(Clone)");
        QGCol = QuestGoal.transform.Find("QuestCollider(Clone)");
        QSCol.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!GeneratedData)
        {
            GenerateData();
            GenerateEnd = true;
        }
    }
    void GenerateData()
    {
        var PS = GameObject.Find("PsManager").GetComponent<PScriptsManager>();
        for (int i = 0; i < PS.chObjData[0].Length; i++)
        {
            //Debug.Log(i+PS.chObjData[0][i].name);
            int temp = Random.Range(0, PS.chObjData[1].Length - 1);
            questList.Add(i, new QuestData(PS.chObjData[0][i].name, i, PS.chObjData[0].Length - 1, PS.chObjData[0][i], PS.chObjData[1][temp], false));//퀘스트 만들기
            //Debug.Log(questList[i].QuestId+questList[i].Goal.name + questList[i].questName);
            //Debug.Log(questList[i].Msgbox);
        }
        //target = DestinationFind.FindComponentInChildWithTag<Transform>(PS.chObjData[0][0].gameObject, "QuestCollider");
        GeneratedData = true;
    }

    public int GetQuestIndex(int id)
    {
        return questId; //+ questActionIndex;
    }//빌딩 아이디 받고 퀘스트 번호 반환하는 함수 + 퀘스트 순서

    public string CheckQuest(int id)
    {
        var PS = GameObject.Find("PsManager").GetComponent<PScriptsManager>();
        if (id <= PS.chObjData[0].Length - 1)
            NextQuest();
        return questList[questId].questName;
    }//퀘스트 순서 증감

    void NextQuest()
    {
        totalClear += 1;
        if (totalClear >= questList.Count)
        {//생성한 퀘스트 모두 클리어시 퀘스트 데이터 재생성하고 0번 퀘스트부터 재시작
            totalClear = 0;
            questList = new Dictionary<int, QuestData>();
            GenerateData();
        }
        questId = Random.Range(0, questList.Count);//0에서 questList 길이-1 까지 난수
        
        while (true)
        {
            if(questList[questId].QuestClear == true)
            {//퀘스트 넘버를 랜덤으로 정하고 이미 클리어한 퀘스트라면 다시 랜덤으로 정한다.
                questId = Random.Range(0, questList.Count);
                //print(questId+"번 이미 클리어");
            }
            else
            {
                //print(questId + "번 진행");
                break;
            }
        }
        
        //Debug.Log(questId);
        QuestStart = questList[questId].Start;
        QuestGoal = questList[questId].Goal;
        
        QSCol = QuestStart.transform.Find("QuestCollider(Clone)");
        QGCol = QuestGoal.transform.Find("QuestCollider(Clone)");
        QSCol.gameObject.SetActive(true);
    }

    public void ConfirmQuest()
    {
        var QP = GameObject.Find("QuestPointer").GetComponent<QuestPointer>();
        var PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollisionManager>();
        
        if (GameObject.Find("GameCanvas").transform.Find("QuestMainUI").gameObject.activeSelf == true)
            GameObject.Find("GameCanvas").transform.Find("QuestMainUI").gameObject.SetActive(false);
        if (GameObject.Find("GameCanvas").transform.Find("QuestSuccessUI").gameObject.activeSelf == true)
            GameObject.Find("GameCanvas").transform.Find("QuestSuccessUI").gameObject.SetActive(false);

        if (QP.reach == true)
        {
            //Debug.Log("목적지");
            QSCol.gameObject.SetActive(false);
            QGCol.gameObject.SetActive(false);
            Transform QSring = DestinationFind.FindComponentInChildWithTag<Transform>(QuestStart.gameObject, "magic_ring");
            Transform QGring = DestinationFind.FindComponentInChildWithTag<Transform>(QuestGoal.gameObject, "magic_ring");
            //Debug.Log(QSring); Debug.Log(QGring);

            GameObject.Find("ScoreUI").transform.Find("GoldText").GetComponent<MoneyManager>().money += int.Parse(TortalReward.text);
            if (QSring != null)
                Destroy(QSring.gameObject);
            if (QGring != null)
                Destroy(QGring.gameObject);//목적지 퀘스트 완료시 기존에 생성한 푸른색 링 삭제

            questList[questId].QuestClear = true;//퀘스트 클리어 정보 저장
            PC.QuestSet = false;
            QP.reach = false;
            QP.TimeAct = false;
            CheckQuest(questId);
            var GT = GameObject.Find("GameCanvas").transform.Find("GameTime").GetComponent<GameTime>();
            if (Level == "Easy")
            {
                EClear += 1;
                //GT.time += 5f;
            }
            else if (Level == "Normal")
            {
                NClear += 1;
                //GT.time += 8f;
            }
            else
            {
                HClear += 1;
                //GT.time += 10f;
            }
        }
        else
        {
            QGCol.gameObject.SetActive(true);
            PC.QuestSet = true;
            QP.reach = true;
            QP.time = QuestTime;
            QP.TimeAct = true;
            QP.CanCount = true;
            //QuestData
            float temp = Vector3.Distance(QuestStart.gameObject.transform.position, QuestGoal.gameObject.transform.position);
            float distance = Mathf.Abs(temp);//거리 계산(절대값)
            //Debug.Log(distance);
            var GT = GameObject.Find("GameCanvas").transform.Find("GameTime").GetComponent<GameTime>();
            if (Level == "Easy")
            {
                GT.time += 5f;
            }
            else if (Level == "Normal")
            {
                GT.time += 12f;
            }
            else
            {
                GT.time += 15f;
            }
        }
    }
}
