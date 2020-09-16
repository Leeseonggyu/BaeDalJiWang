using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    Text BlinkText;
    // Start is called before the first frame update
    void Start()
    {
        BlinkText = GetComponent<Text>();
        StartCoroutine(DoneBlink());
    }

    public IEnumerator DoneBlink()
    {
        while (true)
        {
            BlinkText.text = "";
            yield return new WaitForSeconds(.5f);
            BlinkText.text = "화면을 터치해주세요";
            yield return new WaitForSeconds(1.0f);
        }
    }
}
