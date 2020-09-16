using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScript : MonoBehaviour
{
    public List<Dictionary<string, object>> data;
    public Text BrotherText, MainCharacterText;
    bool activeSay;
    float PrintTime;
    void Awake()
    {
        data = CSVReader.Read("Main_and_Brother_s_talking_script");
        /*
        for (var i = 0; i < data.Count; i++)
        {
            print("ID: " + data[i]["ID"] + " " +
                   "MsgTime: " + data[i]["MsgTime"] + " " +
                   "Msgbox: " + data[i]["Msgbox"]);
        }
        
        for (var i = 0; i < data2.Count; i++)
        {
            print("ID: " + data2[i]["ID"] + " " +
                   "QuestName: " + data2[i]["QuestName"] + " " +
                   "MsgboxText: " + data2[i]["MsgboxText"].ToString());
        }*/
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        activeSay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeSay == true)
        {
            PrintTime -= Time.deltaTime;
            if (PrintTime <= 0)
            {
                if (GameObject.Find("LobbyCanvas").transform.Find("BrotherCharacterSay").gameObject.activeSelf == true)
                    GameObject.Find("LobbyCanvas").transform.Find("BrotherCharacterSay").gameObject.SetActive(false);
                if (GameObject.Find("LobbyCanvas").transform.Find("MainCharacterSay").gameObject.activeSelf == true)
                    GameObject.Find("LobbyCanvas").transform.Find("MainCharacterSay").gameObject.SetActive(false);
                activeSay = false;
            }
        }
    }
    public void ClickBrother()
    {
        int temp = Random.Range(data.Count / 2, data.Count);
        if (GameObject.Find("LobbyCanvas").transform.Find("MainCharacterSay").gameObject.activeSelf == true)
            GameObject.Find("LobbyCanvas").transform.Find("MainCharacterSay").gameObject.SetActive(false);

        PrintTime = int.Parse(data[temp]["MsgTime"].ToString());
        BrotherText.text = data[temp]["Msgbox"].ToString();

        GameObject.Find("LobbyCanvas").transform.Find("BrotherCharacterSay").gameObject.SetActive(true);
        activeSay = true;
    }
    public void ClickMaincharacter()
    {
        int temp = Random.Range(0, data.Count / 2);
        if (GameObject.Find("LobbyCanvas").transform.Find("BrotherCharacterSay").gameObject.activeSelf == true)
            GameObject.Find("LobbyCanvas").transform.Find("BrotherCharacterSay").gameObject.SetActive(false);
        PrintTime = int.Parse(data[temp]["MsgTime"].ToString());
        MainCharacterText.text = data[temp]["Msgbox"].ToString();

        GameObject.Find("LobbyCanvas").transform.Find("MainCharacterSay").gameObject.SetActive(true);
        activeSay = true;
    }
}
