using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public string animalName;
        public int time;
    }

    private string playerName;
    public string PlayerName // ENCAPSULATION
    {
        get { return playerName; }
        set
        {
            if (value.Length >= 1 && value.Length <= 8)
            {
                playerName = value;
            }
        }
    }
    public string highScoreName { get; private set; } // ENCAPSULATION
    public string highScoreAnimal { get; private set; } // ENCAPSULATION
    public int highScoreTime { get; private set; } // ENCAPSULATION
    public static DataManager Instance { get; private set; } // ENCAPSULATION
    private GameObject selectedPrefab;
    public GameObject SelectedPrefab // ENCAPSULATION
    {
        get { return selectedPrefab; }
        set
        {
            if (value != null)
            {
                selectedPrefab = value;
            }
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            highScoreTime = 9999;
            LoadHighScore();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveHighScore(int newTime)
    {
        if (newTime < highScoreTime)
        {
            SaveData data = new SaveData();
            data.playerName = playerName;
            data.animalName = selectedPrefab.name;
            data.time = newTime;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
        }
    }

    private void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreName = data.playerName;
            highScoreAnimal = data.animalName;
            highScoreTime = data.time;
        }
    }
}
