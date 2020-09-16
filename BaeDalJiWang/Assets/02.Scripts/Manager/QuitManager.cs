using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitManager : MonoBehaviour
{
    public static QuitManager instance;
    string sceneName;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        DontDestroyOnLoad(instance);
    }
    private void OnDisable()
    {
        Destroy(instance);
    }

    public void ClickQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void ClickRetuenButton()
    {
        GameObject.Find("QuitM").transform.Find("QuitCanvas").gameObject.SetActive(false);
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "StartScene" && sceneName != "LobbyScene")
        {
            var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPlayer>();
            camera.pause = false;//카메라 일시정지 해제
        }

        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GameObject.Find("QuitM").transform.Find("QuitCanvas").gameObject.SetActive(true);
            sceneName = SceneManager.GetActiveScene().name;
            if (sceneName != "StartScene" && sceneName != "LobbyScene")
            {
                var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPlayer>();
                camera.pause = true;//카메라 추적 중지 방지

                Time.timeScale = 0;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
        }
    }
}
