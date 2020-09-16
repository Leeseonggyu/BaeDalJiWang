using UnityEngine;

public class ToLobby : MonoBehaviour
{
    bool change = true;
    // Start is called before the first frame update
    void Update()
    {
        if (change == true)
        {
            LoadingScreenManager.Instance.LoadScene("LobbyScene");
            change = false;
        }
    }
}
