using UnityEngine;

public class DataManager : MonoBehaviour
{
    private string playerName;
    public string PlayerName
    {
        get { return playerName; }
        set
        {
            if (value.Length > 1 && value.Length < 8)
            {
                playerName = value;
            }
        }
    }
    public static DataManager Instance { get; private set; }
    private GameObject selectedPrefab;
    public GameObject SelectedPrefab
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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
