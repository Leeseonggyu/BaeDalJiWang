using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    public Text LeftGameText;
    public float time;
    float overTime, firstTime;
    float unTime;
    public GameObject IamMove, ReadyMove, LetMove, goMove;
    public bool act;
    // Start is called before the first frame update
    void Start()
    {
        time = 120; overTime = 0; firstTime = 0; act = true;
        unTime = 0;
        LeftGameText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //var cd = GameObject.Find("GameCanvas").transform.Find("Restart Count Down UI").transform.Find("Text").GetComponent<CountDown>();
        //var QP = GameObject.Find("QuestPointer").GetComponent<QuestPointer>();

        if (act == true)
        {
            Invoke("GUImove", 1f);
        }

        if (time > 0)
        {
            LeftGameText.text = "게임 시간: " + string.Format("{0:N0}", time);
        }
        else if (overTime == 0)
        {
            var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPlayer>();
            camera.pause = true;//카메라 추적 중지 방지

            Time.timeScale = 0;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            var Pmove = GameObject.FindWithTag("Player").GetComponent<Move>();
            Pmove.moveDir = Direction.Stop;

            overTime -= Time.unscaledDeltaTime;
            GameObject TO = GameObject.Find("GameCanvas").transform.Find("GUICanvas").transform.Find("TIMEOVER").gameObject;
            TO.SetActive(true);
            TO.transform.Find("TOMove").GetComponent<GUICanvas>().SetUpStart();
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            Invoke("ActScreen", 0.5f);
        }
    }

    private void FixedUpdate()
    {
        var cd = GameObject.Find("GameCanvas").transform.Find("Restart Count Down UI").transform.Find("Text").GetComponent<CountDown>();
        if (!cd.act && act == false)
        {
            time -= Time.deltaTime;
        }
        else
        {
            var Pmove = GameObject.FindWithTag("Player").GetComponent<Move>();
            Pmove.SpeedLevel = 0;
            Pmove.moveDir = Direction.Stop;
            Pmove.ManageSpeed();
        }
    }
    void ActScreen()
    {
        GameObject.Find("GameCanvas").transform.Find("ResultScreen").gameObject.SetActive(true);
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    private void GUImove()
    {
        unTime += Time.unscaledDeltaTime;
        var Iam = IamMove.GetComponent<GUICanvas>();
        var Ready = ReadyMove.GetComponent<GUICanvas>();
        var Let = LetMove.GetComponent<GUICanvas>();
        var Go = goMove.GetComponent<GUICanvas>();

        if (Iam.end == false && firstTime == 0 && unTime >= 0)
        {
            GameObject.Find("GameCanvas").transform.Find("GUICanvas").transform.Find("I AM").gameObject.SetActive(true);
            IamMove.GetComponent<GUICanvas>().SetUpStart();
            firstTime += 1;
        }
        else if (Ready.end == false && firstTime == 1 && unTime >= 0)
        {
            GameObject.Find("GameCanvas").transform.Find("GUICanvas").transform.Find("READY").gameObject.SetActive(true);
            ReadyMove.GetComponent<GUICanvas>().SetUpStart();
            firstTime += 1;
        }
        else if (Let.end == false && firstTime == 2 && unTime >= 3)
        {
            GameObject.Find("GameCanvas").transform.Find("GUICanvas").transform.Find("I AM").gameObject.SetActive(false);
            GameObject.Find("GameCanvas").transform.Find("GUICanvas").transform.Find("READY").gameObject.SetActive(false);

            GameObject.Find("GameCanvas").transform.Find("GUICanvas").transform.Find("let's").gameObject.SetActive(true);
            LetMove.GetComponent<GUICanvas>().SetUpStart();
            firstTime += 1;
        }
        else if (Go.end == false && firstTime == 3 && unTime >= 3)
        {
            GameObject.Find("GameCanvas").transform.Find("GUICanvas").transform.Find("go").gameObject.SetActive(true);
            goMove.GetComponent<GUICanvas>().SetUpStart();
            firstTime += 1;
            Invoke("wait", 2f);
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else if (firstTime == 4 && unTime >= 5)
        {
            GameObject.Find("GameCanvas").transform.Find("GUICanvas").transform.Find("let's").gameObject.SetActive(false);
            GameObject.Find("GameCanvas").transform.Find("GUICanvas").transform.Find("go").gameObject.SetActive(false);
            firstTime += 1;
            act = false;
            unTime = 0;
        }
    }
    void wait()
    {}
}
