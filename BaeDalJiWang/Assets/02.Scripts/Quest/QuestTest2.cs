using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTest2 : MonoBehaviour
{
    private float LimitTime;      // 퀘스트 지역에서 몇 초 정차했는지 확인하는 시간
    GameObject QuestGUI;          // 퀘스트 창 GUI 오브젝트
    float QuestTime = 3f;  // 퀘스트 지역에서 3초 동안 정차해 있어야 퀘스트 수락 // public 으로 만들어 컴포넌트에서 관리 가능하게 하는 것 고려
    bool QuestSet;        // 퀘스트 진행 중인지 확인하는 값

    // Start is called before the first frame update
    void Start()
    {
        QuestGUI = GameObject.Find("QuestCanvas").transform.Find("Quest").gameObject;  // Quset GUI 불러오는 줄
        QuestGUI.gameObject.GetComponent<QuestTest2>();
        QuestSet = false;
    }



    private void OnTriggerStay(Collider other)   // 플레이어가 트리거 안에 들어왔는지 프레임마다 확인
    {
        //Debug.Log("Stay");
        if (QuestSet == false)  // 플레이어 충돌 확인 // 퀘스트 진행 중인지 확인
        {
            Debug.Log("정차");
            LimitTime += Time.deltaTime;  // 정차 시간

            if (QuestTime < LimitTime)
            {
                Debug.Log("멈춤");
                //questManager.QuestCheck(false);
                QuestGUI.SetActive(true);   // GUI 활성화
                QuestSet = true;            // 퀘스트 진행 중
            }
        }
    }




    private void OnTriggerExit(Collider other)
    {
        Debug.Log("나감");

        LimitTime = 0f;                 // 정차 시간 초기화
        //QuestGUI.SetActive(false);      // GUI 비활성화
        QuestSet = false;
    }

}
