using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowTimeText : MonoBehaviour
{
    Text TimeText;
    // Start is called before the first frame update
    void Start()
    {
        TimeText = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        TimeText.text = System.DateTime.Now.ToString("HH:mm tt");
    }
}
