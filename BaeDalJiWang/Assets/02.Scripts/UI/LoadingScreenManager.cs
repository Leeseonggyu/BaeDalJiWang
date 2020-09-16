using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
#pragma warning disable 0649

public class LoadingScreenManager : MonoBehaviour
{
    private static LoadingScreenManager instance;
    public Sprite s0, s1, s2, s3, s4, s5, s6;
    public Sprite[] Loadingimages;
    public RectTransform motorImage;

    public static LoadingScreenManager Instance
    {//싱글톤 패턴
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<LoadingScreenManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }
            return instance;
        }
    }

    private static LoadingScreenManager Create()
    {
        return Instantiate(Resources.Load<LoadingScreenManager>("LoadingUI"));
    }

    private void Awake()
    {
        if(Instance!=this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void resetImage()
    {
        Loadingimages = new Sprite[7];
        Loadingimages[0] = s0;
        Loadingimages[1] = s1;
        Loadingimages[2] = s2;
        Loadingimages[3] = s3;
        Loadingimages[4] = s4;
        Loadingimages[5] = s5;
        Loadingimages[6] = s6;
    }
    private void Start()
    {
        resetImage();
    }

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Image progressBar;

    private string loadSceneName;

    public void LoadScene(string sceneName)
    {
        //resetImage();
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            instance.GetComponent<Image>().sprite = Loadingimages[0];
        }
        else if (SceneManager.GetActiveScene().name == "LobbyScene" || SceneManager.GetActiveScene().name == "DummyMap" || SceneManager.GetActiveScene().name == "GameScene")
        {
            System.Random randImage = new System.Random();
            int rnum = randImage.Next(1, Loadingimages.Length);
            instance.GetComponent<Image>().sprite = Loadingimages[rnum];
        }
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        loadSceneName = sceneName;
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        progressBar.fillAmount = 0f;
        if (loadSceneName == "GameScene")
        {
            progressBar.fillAmount = 1f;//비동기화 불가
        }
        yield return StartCoroutine(Fade(true));
        //FadeIn 끝남
        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);//비동기 씬 로딩 시작
        op.allowSceneActivation = false;//씬 로딩이 끝날시 바로 넘어가는 현상 방지
        
        float timer = 0f;
        while (!op.isDone)//로딩 완료시까지 반복
        {
            yield return null;
            if (op.progress < 0.9f)
            {//When allowSceneActivation is set to false then progress is stopped at 0.9. The isDone is then maintained at false. When allowSceneActivation is set to true isDone can complete.
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

                //Debug.Log(progressBar.fillAmount);
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
            motorImage.anchoredPosition = new Vector2(100+500 * progressBar.fillAmount, 300);
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name==loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private IEnumerator Fade(bool isFadeIn)//씬 로딩을 시작하거나 끝낼 때 로딩 UI를 페이드 인 페이드 아웃으로 자연스럽게 나타나고 사라지게 만드는 함수
    {
        float timer = 0f;
        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 3f;
            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);
        }//isFadeIn(true is fadeIn-false is fadeOut)
        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }
}
