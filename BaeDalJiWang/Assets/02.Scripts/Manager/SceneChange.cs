using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void StartScene()
    {
        LoadingScreenManager.Instance.LoadScene("LobbyScene");
    }

    public void LobbyScene()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        LoadingScreenManager.Instance.LoadScene("DummyMap");
    }
}
