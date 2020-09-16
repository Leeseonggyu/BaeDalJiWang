using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//PrintScripts
public class PScriptsManager : MonoBehaviour
{
    public Dictionary<int, string[]>[] objDataP;
    public Dictionary<int, string[]> TempD;
    public ObjData[][] chObjData;
    private ObjData[] objtemp;


    // Start is called before the first frame update
    int countNone;
    
    public void SetUpStart()
    {
        //objDataP = new Dictionary<int, string[]>[100];
        GenerateData();
        var QM = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        QM.SetUpStart();
    }
    public void GenerateData()
    {
        var OD = GameObject.FindGameObjectWithTag("BuildingManager").GetComponent<ObjDataManager>();

        objDataP = new Dictionary<int, string[]>[OD.ManagerObjT.Length];
        chObjData = new ObjData[OD.ManagerObjT.Length][];
        countNone = 0;

        for (int i = 0; i < OD.ManagerObjT.Length; i++)
        {//데이터 정렬
            
            chObjData[i] = objtemp;
            objtemp = new ObjData[OD.countLength[i]];

            for (int j = 0; j < OD.childObj[i].Length; j++)
            {
                if (OD.childObj[i][j].gameObject.tag == "Building")
                {
                    objtemp[j - countNone] = OD.childObj[i][j].GetComponent<ObjData>();
                    //Debug.Log(j - countNone);
                    //Debug.Log(objtemp[j - countNone]+" "+ (j - countNone) +""+ i+" "+j);
                }
                else
                {
                    countNone++;
                }
            }
            countNone = 0;
            chObjData[i] = objtemp;//ID 값을 지닌 오브젝트 배열 저장
        }

        for (int i = 0; i < chObjData.Length; i++)
        {
            //Debug.Log(chObjData[i].Length);
            
            TempD = new Dictionary<int, string[]>();
            for (int j = 0; j < chObjData[i].Length; j++)
            {
                //Debug.Log(chObjData[i][j]);
                //Debug.Log(chObjData[i][j] + "\n" + chObjData[i][j].gameObject.name + "\n" + chObjData[i][j].id);
                //objDataP[i].Add(chObjData[i][j].id, new string[] { chObjData[i][j].gameObject.name, "\n", chObjData[i][j].id.ToString() });
                TempD.Add(chObjData[i][j].id, new string[] { "건물 이름: \n" + chObjData[i][j].gameObject.name, "id: " + chObjData[i][j].id.ToString() });
            }
            //Debug.Log("Gact" + i);
            objDataP[i] = TempD;

        }
    }

    public string GetObjData(int key, int id, int ObjIndex)
    {
        //Debug.Log(key + " " + id + " " + ObjIndex);
        /*
        for (int i = 0; i < objDataP[key][id].Length; i++)
            Debug.Log(objDataP[key][id][i]);
            */
        return objDataP[key][id][0] + "\n"+ objDataP[key][id][1];
    }
}
