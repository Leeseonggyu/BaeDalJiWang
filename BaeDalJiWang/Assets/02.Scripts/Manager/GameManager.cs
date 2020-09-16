using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PScriptsManager PSManager;
    GameObject scanBuilding;
    public GameObject motor;
    public GameObject[] Managers;
    public GameObject MiniMap;
    public GameObject[] PandM = new GameObject[2];//Player and MainCamera
    public QuestManager questManager;
    public bool actPanel, act;
    private bool countDestroy;
    public int objIndex;
    private int i, key;
    public Transform tr;
   

    private void Awake()
    {
        instance = this;
        PandM[0] = GameObject.FindGameObjectWithTag("Player");
        PandM[1] = GameObject.FindGameObjectWithTag("MainCamera");
        for (int i = 0; i < 2; i++)
            DontDestroyOnLoad(PandM[i]);
        Management_MG_Obj();
        //Management_MG_Obj();
        SceneManager.LoadScene("GameScene");
    }
    void Management_MG_Obj()
    {
        Managers = GameObject.FindGameObjectsWithTag("GameController");
        for (i = 0; i < Managers.Length; i++)
        {
            DontDestroyOnLoad(Managers[i]);//게임 관련 매니저 오브젝트들은 씬이 바뀌어도 파괴되지 않고 유지 된다.
            for (int j = 0; j < Managers.Length; j++)
            {
                if (Managers[i].gameObject.name == Managers[j].gameObject.name && i != j && i == 0)//이름을 기준으로 중복되는 오브젝트 삭제 같은 종류의 매니저가 2개 이상 존재할 필요 없음 기능 정지
                {//i번째 오브젝트가 이전 오브젝트와 같은 이름인지 판별 첫번째로 넣어진 오브젝트인 경우 판별 제외(무조건 같은 이름으로 판정되기 때문)
                    Destroy(Managers[i]);
                }
            }
        }
    }
    void OnEnable()
    {
        MiniMap = GameObject.FindGameObjectWithTag("MiniMap");
        DontDestroyOnLoad(MiniMap);
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "DummyMap")
        {//더미 맵에서 게임 관리 오브젝트들을 가져옴
            //Debug.Log("Dummy");
            PandM[0] = GameObject.FindGameObjectWithTag("Player");
            PandM[1] = GameObject.FindGameObjectWithTag("MainCamera");
            for (int i = 0; i < 2; i++)
                DontDestroyOnLoad(PandM[i]);
            Management_MG_Obj();
            act = false;
        }
        if (scene.name == "GameScene")
        {
            //GameObject.FindGameObjectWithTag("Player").transform.Find("WayPointUI").gameObject.SetActive(true);
            var PS = GameObject.Find("PsManager").GetComponent<PScriptsManager>();
            PS.SetUpStart();
            Transform TempTr = DestinationFind.FindComponentInChildWithTag<Transform>(GameObject.Find("Building_House_06"), "QuestCollider");
            tr.transform.position = new Vector3(TempTr.transform.position.x+5, TempTr.transform.position.y, TempTr.transform.position.z);
        }
        if (scene.name=="LobbyScene")
        {//로비 갈때 유지하던 오브젝트들 파괴
            for (int i = 0; i < 2; i++)
                Destroy(PandM[i]);
            for (int i = 0; i < Managers.Length; i++)
                Destroy(Managers[i]);
            Destroy(MiniMap);
        }
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Action()
    {
        if (act)
        {
            act = false;
        }
        else
        {
            act = true;
        }
    }

}
