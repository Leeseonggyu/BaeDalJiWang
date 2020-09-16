using UnityEngine;

public class GameInfoManager : MonoBehaviour
{
    public static GameInfoManager instance;
    public int recentScore;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
}
