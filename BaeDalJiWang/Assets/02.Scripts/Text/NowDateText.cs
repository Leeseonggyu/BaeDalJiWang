using UnityEngine;
using UnityEngine.UI;

public class NowDateText : MonoBehaviour
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
        TimeText.text = System.DateTime.Now.ToString("yyyy.MM.dd");
    }
}
