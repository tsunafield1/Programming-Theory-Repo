using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class TitleUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject catPrefab;
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private TextMeshProUGUI highScoreText;

    void Start()
    {
        highScoreText.text = $"{DataManager.Instance.highScoreName}\nAnimal: {DataManager.Instance.highScoreAnimal}\nTime: {DataManager.Instance.highScoreTime}";
    }

    public void OnEndEditName(string newName)
    {
        DataManager.Instance.PlayerName = newName;
    }

    public void OnCatClicked()
    {
        OnAnimalSelected(catPrefab);
    }

    public void OnBirdClicked()
    {
        OnAnimalSelected(birdPrefab);
    }

    private void OnAnimalSelected(GameObject selectedAnimalPrefab)
    {
        DataManager.Instance.SelectedPrefab = selectedAnimalPrefab;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Exit();
#endif
    }
}
