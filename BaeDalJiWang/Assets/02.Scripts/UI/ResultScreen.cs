using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    public Text Eclear, Nclear, Hclear;
    public Text TortalClear, TortalMoney;
    public Text Rank;
    private void OnEnable()
    {
        if (GameObject.Find("Minimap").transform.Find("CanvasMiniMap").gameObject.activeSelf == true)
            GameObject.Find("Minimap").transform.Find("CanvasMiniMap").gameObject.SetActive(false);

        var QM = GameObject.Find("QuestManager").GetComponent<QuestManager>();

        Eclear.text = "쉬움 : "+QM.EClear.ToString();
        Nclear.text = "보통 : " + QM.NClear.ToString();
        Hclear.text = "어려움 : " + QM.HClear.ToString();

        int money = GameObject.Find("ScoreUI").transform.Find("GoldText").GetComponent<MoneyManager>().money;
        TortalClear.text = "총 배달 횟수 : " + QM.totalClear.ToString();
        TortalMoney.text = "총 금액 : " + money;

        GameInfoManager.instance.recentScore = money;
        if (money < 20000)
        {
            Rank.text = "D";
        }
        else if (money < 30000)
        {
            Rank.text = "C";
        }
        else if (money < 40000)
        {
            Rank.text = "B";
        }
        else if (money < 50000)
        {
            Rank.text = "A";
        }
        else if (money < 70000)
        {
            Rank.text = "A+";
        }
        else
        {
            Rank.text = "S";
        }
    }
}
