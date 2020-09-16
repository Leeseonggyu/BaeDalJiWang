using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedText : MonoBehaviour
{
    Text speedtext;
    // Start is called before the first frame update
    void Start()
    {
        speedtext = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var Pmove = GameObject.FindWithTag("Player").GetComponent<Move>();
        if (Pmove.maxSpeed)
            speedtext.text = "Speed Level\n Max";
        else
            speedtext.text = "Speed Level\n" + Pmove.SpeedLevel + "단";
    }
}
