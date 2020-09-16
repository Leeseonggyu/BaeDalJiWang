using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicUI : MonoBehaviour
{
    public Slider backVolume;
    public new AudioSource[] audio;
    private Image SoundUI;
    public Sprite ImageMute;
    public Sprite ImageUnMute;
    public bool Unmute;
    private float backvol = 1f;
    public int SongNum;
    public Text SongName;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "LobbyScene")
        {
            SoundUI = GameObject.Find("LobbyCanvas").transform.Find("SettingUI").transform.Find("Image").transform.Find("Toggle").transform.Find("Background").transform.Find("SoundCheckmark").GetComponent<Image>();
            SongNum = 0;
        }
        else
        {
            SoundUI = GameObject.Find("GameCanvas").transform.Find("SettingUI").transform.Find("Image").transform.Find("Toggle").transform.Find("Background").transform.Find("SoundCheckmark").GetComponent<Image>();
            SongNum = Random.Range(0, audio.Length);
        }
        Unmute = true;
        backvol = PlayerPrefs.GetFloat("backvol", 1f);
        backVolume.value = backvol;
        
        audio[SongNum].Play();
        SongName.text = audio[SongNum].name;
        audio[SongNum].volume = backVolume.value;
        //Debug.Log(audio.Length);
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(audio[SongNum].isPlaying);
        if (!audio[SongNum].isPlaying)
            NextSong();
        if (Unmute == true)
            SoundSlider();
    }

    public void SoundSlider()
    {
        audio[SongNum].volume = backVolume.value;

        backvol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", 1f);
    }
    public void callMute()
    {
        Mute();
        if (Unmute == false)
            Unmute = true;
        else if(Unmute == true)
            Unmute = false;
    }

    private void Mute()
    {
        if (Unmute == true)
        {
            //Debug.Log("Mute");
            audio[SongNum].volume = 0;
            backvol = 0;
            PlayerPrefs.SetFloat("backvol", 0f);
            SoundUI.sprite = ImageMute;
        }
        else if (Unmute == false) 
        {
            //Debug.Log("UnMute");
            audio[SongNum].volume = backVolume.value;
            backvol = backVolume.value;
            PlayerPrefs.SetFloat("backvol", 1f);
            SoundUI.sprite = ImageUnMute;
        }
    }

    public void NextSong()
    {
        audio[SongNum].Stop();
        SongNum++;
        if (SongNum >= audio.Length)
            SongNum = 0;
        //Debug.Log(SongNum);
        audio[SongNum].Play();
        SongName.text = audio[SongNum].name;
        if (Unmute == false)
        {
            audio[SongNum].volume = 0;
            backvol = 0;
            PlayerPrefs.SetFloat("backvol", 0f);
        }
    }
    public void BackSong()
    {
        audio[SongNum].Stop();
        SongNum--;
        if (SongNum < 0)
            SongNum = audio.Length - 1;
        //Debug.Log(SongNum);
        audio[SongNum].Play();
        SongName.text = audio[SongNum].name;
        if (Unmute == false)
        {
            audio[SongNum].volume = 0;
            backvol = 0;
            PlayerPrefs.SetFloat("backvol", 0f);
        }
    }
}
