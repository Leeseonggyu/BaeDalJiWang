using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjDataManager : MonoBehaviour
{
    public GameObject ring;
    public GameObject[] managerObjG;
    public Transform[] ManagerObjT;
    public Transform[][] childObj;//인스펙터 창에는 보이지 않는 2차원 배열 자동 할당 및 계산용
    public Transform[] child1, child2;//인스펙터 창에 일단 보여주기 위해서 수동 할당 그저 보여주기 위한 것으로 게임에는 영향을 주지 않음
    public int[] countLength;
    int countNone = 0;
    public int index = 0;

    private void Awake()
    {
        managerObjG = GameObject.FindGameObjectsWithTag("BuildM");
        ManagerObjT = new Transform[managerObjG.Length];
        childObj = new Transform[managerObjG.Length][];
        countLength = new int[managerObjG.Length];
        countNone = 0;

        for (int i = 0; i < managerObjG.Length; i++)
        {
            ManagerObjT[i] = managerObjG[i].GetComponent<Transform>();
            
            childObj[i]= ManagerObjT[i].GetComponentsInChildren<Transform>();//자식 오브젝트 트랜스폼 자동 부여;

            for (int j = 1; j < childObj[i].Length; j++)//0을 뺀 이유 매니저가 들어가 있음
            {
                var chObjData = childObj[i][j].gameObject.GetComponent<ObjData>();

                //Debug.Log("i: " + i + " j: " + j);
                if (childObj[i][j].gameObject.tag == "Building")//모든 자식 오브젝트를 받아와서 자식의 자식까지 아이디를 부여하는 현상 방지
                {
                    chObjData.id = j - countNone;//ID를 부여할 필요가 없는 오브젝트를 카운트해서 뺸다. 모든 오브젝트에 대해 ID를 부여할 변수 i 값이 증가하기에 필요한 조치
                    //Debug.Log("id: " + chObjData.id);
                    //Debug.Log("Name: " + childObj[i][j].gameObject.name);
                }
                else
                {
                    var tempObject = ring;
                    if(childObj[i][j].gameObject.tag == "QuestCollider")
                    {
                        Transform tempTr = childObj[i][j].GetComponent<Transform>();
                        tempTr.position = new Vector3(childObj[i][j].transform.position.x, 0.2f, childObj[i][j].transform.position.z);
                        tempObject = Instantiate(ring, tempTr.position, Quaternion.Euler(0, 0, 0));
                        tempObject.transform.parent = tempTr.gameObject.transform.parent;
                        tempObject.SetActive(false);
                    }
                    countNone++;
                    //Debug.Log("countNone: " + countNone);
                }
            }
            countLength[i] = childObj[i].Length - countNone - 1;
            //Debug.Log("길이: " + countLength[i]);
            countNone = 0;
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            child1 = childObj[0];
            child2 = childObj[1];
        }
    }
}
