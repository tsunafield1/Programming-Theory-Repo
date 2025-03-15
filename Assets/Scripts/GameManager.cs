using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int numberOfCollectibles = 0;
    private int maxCollectibles;
    private float startTime;
    [SerializeField] private float cameraSpeed = 10.0f;
    private bool isGameover;
    [SerializeField] private Vector3 spawnPosition = new Vector3(0, 1, 0);
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 3.75f, -7.5f);
    private GameObject playerObject;
    [SerializeField] private GameObject gameoverScreen;
    [SerializeField] private TextMeshProUGUI collectiblesText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI gameoverText;
    private Animal player;

    void Start()
    {
        // Set up player
        GameObject playerPrefab = DataManager.Instance.SelectedPrefab;
        playerObject = Instantiate(playerPrefab, spawnPosition, playerPrefab.transform.rotation);
        player = playerObject.GetComponent<Animal>();

        // Set up camera
        Camera.main.transform.position = GetTargetCameraPosition();

        // Count collectible
        maxCollectibles = FindObjectsByType<Collectible>(FindObjectsSortMode.None).Length;

        // Set a start time
        startTime = Time.time;

        // Set up UI
        UpdateUI();
    }

    void Update()
    {
        if (!isGameover)
        {
            player.Move();
            MoveCamera();
            UpdateTimeUI();
        }
    }

    // Increase number of collectibles by 1
    public void Collected()
    {
        numberOfCollectibles++;
        UpdateCollectiblesUI();
        if (numberOfCollectibles == maxCollectibles)
        {
            isGameover = true;
            float usedTime = Time.time - startTime;

            gameoverText.text = $"Name: {DataManager.Instance.PlayerName}\nAnimal: {playerObject.name}\nTime: {Mathf.Round(usedTime)}";
            gameoverScreen.SetActive(true);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Get Camera position
    private Vector3 GetTargetCameraPosition()
    {
        return playerObject.transform.position + cameraOffset;
    }

    // Update UI text to current value
    private void UpdateUI()
    {
        UpdateCollectiblesUI();
        UpdateTimeUI();
    }

    private void UpdateCollectiblesUI()
    {
        collectiblesText.text = $"Collectibles: {numberOfCollectibles}/{maxCollectibles}";
    }

    private void UpdateTimeUI()
    {
        timeText.text = $"Time: {Mathf.Round(Time.time - startTime)}";
    }

    // Move camera to player
    private void MoveCamera()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, GetTargetCameraPosition(), cameraSpeed * Time.deltaTime);
        Camera.main.transform.LookAt(playerObject.transform.position);
    }
}
