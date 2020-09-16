using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    public void HowToPlayButton()
    {
        GameObject.Find("LobbyCanvas").transform.Find("HowToPlay").gameObject.SetActive(true);
    }

    public void HowToPlayCloseButton()
    {
        GameObject.Find("LobbyCanvas").transform.Find("HowToPlay").gameObject.SetActive(false);
    }
    public void CreditsButton()
    {
        GameObject.Find("LobbyCanvas").transform.Find("Credits").gameObject.SetActive(true);
    }

    public void CreditsCloseButton()
    {
        GameObject.Find("LobbyCanvas").transform.Find("Credits").gameObject.SetActive(false);
    }

    #region StartButton
    public void ClickStartButton()
    {
        var RC = GameObject.Find("StartButton").GetComponent<transparencyControl>();

        if (RC.RecentClick == false)
        {
            RC.RecentClickButton(GameObject.Find("StartButton"));
            RC.RecentClick = true;
        }
        else
        {
            RC.RecentClickButton(GameObject.Find("StartButton"));
            RC.RecentClick = false;
        }

        GameObject.Find("LobbyCanvas").transform.Find("ConfirmStart").gameObject.SetActive(true);
    }

    public void ConfirmStartNoneButton()
    {
        GameObject.Find("LobbyCanvas").transform.Find("ConfirmStart").gameObject.SetActive(false);
        var RC = GameObject.Find("StartButton").GetComponent<transparencyControl>();

        RC.RecentClick = true;
        RC.RecentClickButton(GameObject.Find("StartButton"));
        RC.RecentClick = false;
    }

    public void ConfirmStartYesButton()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        LoadingScreenManager.Instance.LoadScene("DummyMap");
    }
    #endregion

    #region StuntButton
    public void ClickStuntButton()
    {
        var RC = GameObject.Find("StuntButton").GetComponent<transparencyControl>();

        if (RC.RecentClick == false)
        {
            RC.RecentClickButton(GameObject.Find("StuntButton"));
            RC.RecentClick = true;
        }
        else
        {
            RC.RecentClickButton(GameObject.Find("StuntButton"));
            RC.RecentClick = false;
        }
        GameObject.Find("LobbyCanvas").transform.Find("ConfirmRace").gameObject.SetActive(true);
    }

    public void StuntCloseButton()
    {
        GameObject.Find("LobbyCanvas").transform.Find("ConfirmRace").gameObject.SetActive(false);
        var RC = GameObject.Find("StuntButton").GetComponent<transparencyControl>();

        RC.RecentClick = true;
        RC.RecentClickButton(GameObject.Find("StuntButton"));
        RC.RecentClick = false;
    }
    #endregion

    #region InventoryButton
    public void ClickInventoryButton()
    {
        var RC = GameObject.Find("InventoryButton").GetComponent<transparencyControl>();

        if (RC.RecentClick == false)
        {
            RC.RecentClickButton(GameObject.Find("InventoryButton"));
            RC.RecentClick = true;
        }
        else
        {
            RC.RecentClickButton(GameObject.Find("InventoryButton"));
            RC.RecentClick = false;
        }
        GameObject.Find("InventoryCanvas").transform.Find("Clothes Canvas").gameObject.SetActive(true);
    }
    public void InventoryCloseButton()
    {
        if(GameObject.Find("InventoryCanvas").transform.Find("Clothes Canvas").gameObject.activeSelf == true)
            GameObject.Find("InventoryCanvas").transform.Find("Clothes Canvas").gameObject.SetActive(false);
        else if (GameObject.Find("InventoryCanvas").transform.Find("Helemt Canvas").gameObject.activeSelf == true)
            GameObject.Find("InventoryCanvas").transform.Find("Helemt Canvas").gameObject.SetActive(false);
        else if (GameObject.Find("InventoryCanvas").transform.Find("Motorcycle Canvas").gameObject.activeSelf == true)
            GameObject.Find("InventoryCanvas").transform.Find("Motorcycle Canvas").gameObject.SetActive(false);

        var RC = GameObject.Find("InventoryButton").GetComponent<transparencyControl>();

        RC.RecentClick = true;
        RC.RecentClickButton(GameObject.Find("InventoryButton"));
        RC.RecentClick = false;
    }

    public void ClickHelmetbutton()
    {
        if (GameObject.Find("InventoryCanvas").transform.Find("Clothes Canvas").gameObject.activeSelf == true)
            GameObject.Find("InventoryCanvas").transform.Find("Clothes Canvas").gameObject.SetActive(false);
        else if (GameObject.Find("InventoryCanvas").transform.Find("Motorcycle Canvas").gameObject.activeSelf == true)
            GameObject.Find("InventoryCanvas").transform.Find("Motorcycle Canvas").gameObject.SetActive(false);

        GameObject.Find("InventoryCanvas").transform.Find("Helemt Canvas").gameObject.SetActive(true);

    }
    public void ClickClothesbutton()
    {
        if (GameObject.Find("InventoryCanvas").transform.Find("Helemt Canvas").gameObject.activeSelf == true)
            GameObject.Find("InventoryCanvas").transform.Find("Helemt Canvas").gameObject.SetActive(false);
        else if (GameObject.Find("InventoryCanvas").transform.Find("Motorcycle Canvas").gameObject.activeSelf == true)
            GameObject.Find("InventoryCanvas").transform.Find("Motorcycle Canvas").gameObject.SetActive(false);

        GameObject.Find("InventoryCanvas").transform.Find("Clothes Canvas").gameObject.SetActive(true);
    }
    public void ClickMotorcyclebutton()
    {
        if (GameObject.Find("InventoryCanvas").transform.Find("Helemt Canvas").gameObject.activeSelf == true)
            GameObject.Find("InventoryCanvas").transform.Find("Helemt Canvas").gameObject.SetActive(false);
        else if (GameObject.Find("InventoryCanvas").transform.Find("Clothes Canvas").gameObject.activeSelf == true)
            GameObject.Find("InventoryCanvas").transform.Find("Clothes Canvas").gameObject.SetActive(false);

        GameObject.Find("InventoryCanvas").transform.Find("Motorcycle Canvas").gameObject.SetActive(true);
    }
    #endregion

    #region StoreButton
    public void ClickStoreButton()
    {
        var RC = GameObject.Find("StoreButton").GetComponent<transparencyControl>();

        if (RC.RecentClick == false)
        {
            RC.RecentClickButton(GameObject.Find("StoreButton"));
            RC.RecentClick = true;
        }
        else
        {
            RC.RecentClickButton(GameObject.Find("StoreButton"));
            RC.RecentClick = false;
        }
        GameObject.Find("LobbyCanvas").transform.Find("ConfirmContents").gameObject.SetActive(true);
    }
    
    public void StoreCloseButton()
    {
        GameObject.Find("LobbyCanvas").transform.Find("ConfirmContents").gameObject.SetActive(false);
        var RC = GameObject.Find("StoreButton").GetComponent<transparencyControl>();

        RC.RecentClick = true;
        RC.RecentClickButton(GameObject.Find("StoreButton"));
        RC.RecentClick = false;
    }
    #endregion

    #region BestScore
    public void ClickBestScoreButton()
    {
        var RC = GameObject.Find("BestScoreButton").GetComponent<transparencyControl>();
        
        if (RC.RecentClick == false)
        {
            RC.RecentClickButton(GameObject.Find("BestScoreButton"));
            RC.RecentClick = true;
        }
        else
        {
            RC.RecentClickButton(GameObject.Find("BestScoreButton"));
            RC.RecentClick = false;
        }
        GameObject.Find("LobbyCanvas").transform.Find("BestScoreUI").gameObject.SetActive(true);
    }
    public void BestScoreCloseButton()
    {
        GameObject.Find("LobbyCanvas").transform.Find("BestScoreUI").gameObject.SetActive(false);
        var RC = GameObject.Find("BestScoreButton").GetComponent<transparencyControl>();

        RC.RecentClick = true;
        RC.RecentClickButton(GameObject.Find("BestScoreButton"));
        RC.RecentClick = false;
    }
    #endregion

    #region Setting
    public void ClickSettingButton()
    {
        GameObject.Find("LobbyCanvas").transform.Find("SettingUI").gameObject.SetActive(true);
    }

    public void SettingCloseButton()
    {
        GameObject.Find("LobbyCanvas").transform.Find("SettingUI").gameObject.SetActive(false);
    }
    #endregion

    public void ClickMoneyAddButton()
    {
        MoneyManager.instance.money += 100;
        PlayerPrefs.SetInt("Coin", MoneyManager.instance.money);
    }

}
