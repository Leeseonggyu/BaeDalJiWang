using UnityEngine;
using UnityEngine.UI;

public class GaugeUI : MonoBehaviour
{
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullColor = Color.blue;
    public Color m_FillColor = Color.green;

    // Update is called once per frame
    void Update()
    {
        var Pmove = GameObject.FindWithTag("Player").GetComponent<Move>();
        m_Slider.maxValue = Pmove.JumpDistance;
        m_Slider.value = Pmove.countDistance;
        m_FillImage.color = Color.Lerp(m_FillColor, m_FullColor, Pmove.countDistance / Pmove.JumpDistance);
    }
}
