using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoneyManager : MonoBehaviour
{
    public int money;
    public Text MoneyText;
    public static MoneyManager instance;
    private void Awake()
    {
        instance = this;
        if (SceneManager.GetActiveScene().name == "LobbyScene")
        {
            money = PlayerPrefs.GetInt("Coin");
            money += GameInfoManager.instance.recentScore;
            PlayerPrefs.SetInt("Coin", MoneyManager.instance.money);
        }
        else
            money = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        MoneyText = GetComponent<Text>();
        MoneyText.text = money.ToString();
    }

    private void Update()
    {
        if (MoneyText.text != money.ToString())
        {//머니 갱신&업데이트 최소화
            MoneyText.text = money.ToString();
        }
    }
}
