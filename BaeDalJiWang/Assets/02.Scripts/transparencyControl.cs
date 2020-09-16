using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class transparencyControl : MonoBehaviour
{
    public bool RecentClick;
    Color color;

    private void Start()
    {
        RecentClick = false;
    }
    // Update is called once per frame
    public void RecentClickButton(GameObject Button)
    {
        if (!RecentClick)
        {
            Image spr = Button.GetComponent<Image>();
            Image spr2 = Button.transform.GetChild(0).GetComponent<Image>();
            Text text = Button.transform.GetChild(1).GetComponent<Text>();
            if (Button.name == "StuntButton" || Button.name == "StartButton")
            {
                color = new Color(1, 1, 1, 1);
                spr.color = color; spr2.color = color;
                text.color = new Color(0, 247.0f / 255.0f, 1, 1);
                //Debug.Log("act1");
            }
            else
            {
                color = new Color(1, 1, 1, 1);
                spr.color = color; spr2.color = color;
                text.color = new Color(0, 24.0f / 255.0f, 1, 1);
            }
            //Debug.Log("act1");
        }
        else
        {
            Image spr = Button.GetComponent<Image>();
            Image spr2 = Button.transform.GetChild(0).GetComponent<Image>();
            Text text = Button.transform.GetChild(1).GetComponent<Text>();
            if (Button.name == "StuntButton" || Button.name == "StartButton")
            {
                spr.color = new Color(1, 1, 1, 150.0f / 255.0f);
                spr2.color = new Color(0, 0, 0, 150.0f / 255.0f);
                text.color = new Color(1, 0, 0, 150.0f / 255.0f);
                //Debug.Log("act2");
            }
            else
            {
                spr.color = new Color(1, 1, 1, 1);
                spr2.color = new Color(0, 0, 0, 150.0f / 255.0f);
                text.color = new Color(0, 24.0f / 255.0f, 1, 150.0f / 255.0f);
            }
            //Debug.Log("act2");
        }
    }
}
