using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData//코드에서 불러쓰는 용도 MonoBehaviour 불필요
{
    public string questName, StoreType, QuestType, QuestContents, MsgboxM,MsgboxG,MsgboxLate;//퀘스트 이름, ID타입, 기획 내용 없음, 메세지
    public ObjData Start, Goal;
    public bool QuestClear;
    public int QuestId;//빌딩 배열 정보
    public static string[] D_ID = new string[3] { "D1", "D2", "D3" };
    public static string[] P_ID = new string[2] { "P1", "P2" };
    public static string[] T_ID = new string[2] { "T1", "T2" };
    public static int[] TimeLimit = new int[3] { 15, 20, 25 };
    public int[] Reward = new int[3] { 2000, 3000, 4000 };
    public List<Dictionary<string, object>> Startdata= CSVReader.Read("QuestDB__Start");
    public List<Dictionary<string, object>> Goaldata = CSVReader.Read("QuestDB__Goal");
    public List<Dictionary<string, object>> TimeOverdata = CSVReader.Read("QuestDB__TimeOver");

    void setStartData(int min, int max)
    {
        int index = Random.Range(min, max);
        questName = Startdata[index]["QuestName"].ToString();
        MsgboxM = Startdata[index]["MsgboxText"].ToString();
        MsgboxG = Goaldata[index]["MsgboxText"].ToString();
        MsgboxLate = TimeOverdata[index]["MsgboxText"].ToString();
        //Debug.Log(QuestId+questName + MsgboxG);
    }
    public QuestData(string name, int id, int Length, ObjData start, ObjData goal, bool clear)
    {
        QuestClear = clear;
        QuestId = id;
        Start = start;
        Goal = goal;
        float temp = (float)id / Length;
        if (temp <= 0.5)
        {
            QuestType = D_ID[Random.Range(0, D_ID.Length)];
            if (QuestType == D_ID[0])
            {
                StoreType = "중국집";
                setStartData(0, 3);
            }
            else if (QuestType == D_ID[1])
            {
                StoreType = "일식집";
                setStartData(3, 6);
            }
            else if (QuestType == D_ID[2])
            {
                StoreType = "한식집";
                setStartData(6, 9);
            }
            //Debug.Log(MsgboxM);
            //Debug.Log(id + QuestType + "/" + temp);
        }
        else if (temp <= 0.8)
        {
            QuestType = P_ID[Random.Range(0, P_ID.Length)];
            if (QuestType == P_ID[1])
            {
                StoreType = "회사";
                setStartData(9, 11);
            }
            else
            {
                StoreType = "택배";
                setStartData(11, 13);
            }
            //Debug.Log(id + QuestType + "/" + temp);
        }
        else
        {
            QuestType = T_ID[Random.Range(0, T_ID.Length)];
            if (QuestType == T_ID[1])
            {
                StoreType = "택시";
                setStartData(13, 14);
            }
            else
            {
                StoreType = "택시";
                setStartData(14, 15);
            }
            //Debug.Log(id + QuestType + "/" + temp);
        }
    }//빌딩 데이터 생성자(매개변수 생성자)
}
