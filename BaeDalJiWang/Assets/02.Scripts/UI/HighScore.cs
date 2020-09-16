using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HighScore : MonoBehaviour
{
    public static HighScore instance;
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> hightscoreEntryTransformList;

    private void Awake()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            string value = "{\"highscoreEntryList\":[{\"score\":0},{\"score\":0},{\"score\":0},{\"score\":0},{\"score\":0}]}";
            PlayerPrefs.SetString("HighScore", value);
        }
        AddHighScoreEntry(GameInfoManager.instance.recentScore);
        entryContainer = this.gameObject.transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
        
        string jsonString = PlayerPrefs.GetString("HighScore");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        //Debug.Log(PlayerPrefs.GetString("HighScore"));
        
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {//Sort entry list
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[i].score < highscores.highscoreEntryList[j].score)
                {
                    HighscoreEntry temp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = temp;
                }
            }
        }
        hightscoreEntryTransformList = new List<Transform>();
        foreach(HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, hightscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templeteHeight = 60f;
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templeteHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default: rankString = rank.ToString(); break;
        }
        entryTransform.Find("Rank").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("RankScore").GetComponent<Text>().text = score.ToString();

        entryTransform.Find("BackImage").gameObject.SetActive(rank % 2 == 1);
        entryTransform.Find("BaeDalJiWang").gameObject.SetActive(rank == 1);
        if (rank == 1)
        {
            entryTransform.Find("Rank").GetComponent<Text>().color = Color.red;
            entryTransform.Find("RankScore").GetComponent<Text>().color = Color.red;
        }
        if (rank > 4)
        {
            
        }
        else
        {
            transformList.Add(entryTransform);
        }
        
    }

    public void AddHighScoreEntry(int score)
    {
        //Create
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score };
        //Load
        string jsonString = PlayerPrefs.GetString("HighScore");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        //Add new Entry
        highscores.highscoreEntryList.Add(highscoreEntry);

        //Save
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("HighScore", json);
        PlayerPrefs.Save();
        //Debug.Log(PlayerPrefs.GetString("HighScore"));
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;

    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;

    }
}
