using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class DestinationFind
{
    public static TargetPoint FindComponentInChildWithTag<TargetPoint>(this GameObject parent, string tag) where TargetPoint : Component
    {
        Transform targetPoint = parent.transform;
        foreach (Transform tr in targetPoint)
        {
            if (tr.tag == tag)
            {
                return tr.GetComponent<TargetPoint>();
            }
        }
        return null;
    }
};
public class QuestPointer : MonoBehaviour
{
    public TextMeshProUGUI QuestTimeText;
    public float time;
    bool CanSeeNum;
    public bool CanCount;
    [SerializeField]
    public Transform target;
    public Transform arrowRot;
    public bool reach;
    public bool TimeAct;

    private void Start()
    {
        reach = false;
        TimeAct = false;
        CanSeeNum = false;
        CanCount = true; ;
    }

    public void Update()
    {
        var QM = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        if (QM.GenerateEnd == true)
        {
            if (!reach)
            {
                target = DestinationFind.FindComponentInChildWithTag<Transform>(QM.QuestStart.gameObject, "QuestCollider");
            }
            else
            {
                target = DestinationFind.FindComponentInChildWithTag<Transform>(QM.QuestGoal.gameObject, "QuestCollider");
            }
        }
        if (TimeAct == true)
        {
            if(CanSeeNum==false)
                CanSeeNum = true;
            if(time>=0)
            {
                if (CanCount == true)
                {
                    time -= Time.deltaTime;
                    QuestTimeText.text = string.Format("{0:N0}", time);
                }
            }   
        }
        else if (CanSeeNum)
        {
            QuestTimeText.text = "";
            CanSeeNum = false;
        }

        //Debug.Log(target.transform.position);
        transform.LookAt(target);
    }
}
