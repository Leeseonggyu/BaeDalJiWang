using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void ActivePauseButton(bool act)
    {
        GameObject.Find("GameCanvas").transform.Find("GoingButton").gameObject.SetActive(act);
        GameObject.Find("GameCanvas").transform.Find("SettingButton").gameObject.SetActive(act);
        GameObject.Find("GameCanvas").transform.Find("QuitButton").gameObject.SetActive(act);
    }
    public void ActiveConfirmButton(bool act)
    {
        GameObject.Find("GameCanvas").transform.Find("No Button").gameObject.SetActive(act);
        GameObject.Find("GameCanvas").transform.Find("YesButton").gameObject.SetActive(act);
        GameObject.Find("GameCanvas").transform.Find("ConfirmQuit").gameObject.SetActive(act);
    }
    public void ActiveBackGround(bool act)
    {
        GameObject.Find("GameCanvas").transform.Find("Button's BackGround").gameObject.SetActive(act);
        GameObject.Find("GameCanvas").transform.Find("Button's Back Ground 2").gameObject.SetActive(act);
    }
    public void ClickPauseButton()
    {
        var cd = GameObject.Find("GameCanvas").transform.Find("Restart Count Down UI").transform.Find("Text").GetComponent<CountDown>();
        var GT = GameObject.Find("GameCanvas").transform.Find("GameTime").GetComponent<GameTime>();
        if (!cd.act && !GT.act)
        {
            var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPlayer>();
            camera.pause = true;//카메라 추적 중지 방지

            Time.timeScale = 0;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;//https://docs.unity3d.com/2018.3/Documentation/ScriptReference/Time-timeScale.html

            if (GameObject.Find("Minimap").transform.Find("CanvasMiniMap").gameObject.activeSelf == true)
                GameObject.Find("Minimap").transform.Find("CanvasMiniMap").gameObject.SetActive(false);

            ActiveBackGround(true);
            GameObject.Find("GameCanvas").transform.Find("Button's BackGround").gameObject.GetComponent<Image>().color =
                new Color(195.0f / 255.0f, 195.0f / 255.0f, 195.0f / 255.0f, 200.0f / 255.0f);//배경 밝기 조절
            ActivePauseButton(true);
        }//중복동작 방지
    }

    public void ClickGoingButton()
    {
        ActiveBackGround(false);
        ActivePauseButton(false);
        GameObject.Find("GameCanvas").transform.Find("Restart Count Down UI").gameObject.SetActive(true);
        var cd = GameObject.Find("GameCanvas").transform.Find("Restart Count Down UI").transform.Find("Text").GetComponent<CountDown>();
        cd.act = true;


        if (GameObject.Find("Minimap").transform.Find("CanvasMiniMap").gameObject.activeSelf == false)
            GameObject.Find("Minimap").transform.Find("CanvasMiniMap").gameObject.SetActive(true);
    }

    public void ClickSettingButton()
    {
        GameObject.Find("GameCanvas").transform.Find("SettingUI").gameObject.SetActive(true);
    }
    public void SettingCloseButton()
    {
        GameObject.Find("GameCanvas").transform.Find("SettingUI").gameObject.SetActive(false);
    }

    public void ClickQuitButton()
    {
        GameObject.Find("GameCanvas").transform.Find("Button's BackGround").gameObject.GetComponent<Image>().color =
            new Color(195.0f / 255.0f, 195.0f / 255.0f, 195.0f / 255.0f, 226.0f / 255.0f);//배경 밝기 조절
        ActivePauseButton(false);
        ActiveConfirmButton(true);
    }
    public void ClickNoButton()
    {
        ActiveConfirmButton(false);
        ActivePauseButton(true);
    }
    public void ClickYesButton()
    {
        ActiveBackGround(false);
        ActiveConfirmButton(false);

        var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPlayer>();
        camera.pause = true;//카메라 추적 중지 방지

        Time.timeScale = 0;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        GameObject.Find("GameCanvas").transform.Find("ResultScreen").gameObject.SetActive(true);
    }
    public void ClickJumpStopButton()
    {
        var Pmove = GameObject.FindWithTag("Player").GetComponent<Move>();
        var Pjump = GameObject.FindWithTag("Player").GetComponent<Jump>();

        if(!Pjump.jumpButton)
            Pjump.ButtonClick = true;
        GameObject.Find("GameCanvas").transform.Find("JumpButton").gameObject.SetActive(false);
        Pmove.countDistance = 0;
    }

    public void ClickEllipsisButton()
    {
        if (GameObject.Find("GameCanvas").transform.Find("QuestMainUI").gameObject.activeSelf == true)
            GameObject.Find("GameCanvas").transform.Find("QuestMainUI").gameObject.SetActive(false);
        if (GameObject.Find("GameCanvas").transform.Find("QuestSuccessUI").gameObject.activeSelf == true)
            GameObject.Find("GameCanvas").transform.Find("QuestSuccessUI").gameObject.SetActive(false);
    }
    public void ClickDeliveryCompleteButton()
    {
        GameObject.Find("GameCanvas").transform.Find("QuestSuccessUI").gameObject.SetActive(true);
    }
    public void ClickDeliveryProgressButton()
    {
        GameObject.Find("GameCanvas").transform.Find("QuestSuccessUI").gameObject.SetActive(false);
    }

    public void ClickConfirmButton()
    {
        LoadingScreenManager.Instance.LoadScene("LobbyScene");
    }
}
