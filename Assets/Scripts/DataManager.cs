using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    private GameObject selectedPrefab;
    public GameObject SelectedPrefab
    {
        get { return selectedPrefab; }
        set
        {
            if (selectedPrefab == null)
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
