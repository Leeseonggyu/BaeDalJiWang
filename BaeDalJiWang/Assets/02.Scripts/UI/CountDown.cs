using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CountDown : MonoBehaviour
{
    public Text CountText;
    private float timer;
    public bool end, act;
    // Start is called before the first frame update
    private void Awake()
    {
        var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPlayer>();
        camera.pause = true;//카메라 일시정지 해제
        end = true; act = false;
    }

    void Start()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        timer = 0;
        CountText = GetComponent<Text>();
        var Pmove = GameObject.FindWithTag("Player").GetComponent<Move>();
        Pmove.canCount = false;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        var Pmove = GameObject.FindWithTag("Player").GetComponent<Move>();
        Pmove.canCount = true;
    }

    void resetCount()
    {
        
        timer = 0;
        CountText.text = "3";
    }
    void Update()
    {
        var Pmove = GameObject.FindWithTag("Player").GetComponent<Move>();
        var Pjump = GameObject.FindWithTag("Player").GetComponent<Jump>();

        if (Input.GetMouseButtonDown(0) || !Pjump.jumpButton)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            { }
        }
        if (act)
        {
            if (end)//카운트 다운을 끝마쳤는지 확인하고 맞다면 리셋시킨다.
            {
                resetCount();
                end = false;
            }

            timer += Time.unscaledDeltaTime;
            if (timer <= 0.6)
                CountText.text = "3";
            else if (timer <= 1.0)
                CountText.text = "";
            else if (timer <= 1.6)
                CountText.text = "2";
            else if (timer <= 2.0)
                CountText.text = "";
            else if (timer <= 2.6)
                CountText.text = "1";
            else if (timer <= 3.0)
                CountText.text = "";
            else if (timer <= 3.6)
                CountText.text = "0";
            else
            {
                end = true; act = false;
                var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPlayer>();
                camera.pause = false;//카메라 일시정지 해제
                GameObject.Find("GameCanvas").transform.Find("Restart Count Down UI").gameObject.SetActive(false);
            }
        }
    }
}
