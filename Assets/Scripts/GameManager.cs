using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 10.0f;
    [SerializeField] private Vector3 spawnPosition = new Vector3(0, 1, 0);
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 3.75f, 7.5f);
    private GameObject playerObject;
    private Animal player;

    void Start()
    {
        // Set up player
        GameObject playerPrefab = DataManager.Instance.SelectedPrefab;
        playerObject = Instantiate(playerPrefab, spawnPosition, playerPrefab.transform.rotation);
        player = playerObject.GetComponent<Animal>();

        // Set up camera
        Camera.main.transform.position = GetTargetCameraPosition();
    }

    void Update()
    {
        player.Move();
        MoveCamera();
    }

    // Move camera to player
    private void MoveCamera()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, GetTargetCameraPosition(), cameraSpeed * Time.deltaTime);
        Camera.main.transform.LookAt(playerObject.transform.position);
    }

    // Get Camera position
    private Vector3 GetTargetCameraPosition()
    {
        return playerObject.transform.position + cameraOffset;
    }
}
