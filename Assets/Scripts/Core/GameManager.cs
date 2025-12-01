using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    public CameraMovement mainCamera;
    public PlayerMovement playerMovement;
    public Vector2 startPosition;
    public GameObject map1;
    public GameObject map2;
    public GameObject currentMap;

    public Vector3 coloringGamePosition = new Vector3(41, -3.6f, -10);

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {

    }

    public void OpenMapSelected(GameObject mapToOpen)
    {
        if (currentMap != null)
        {
            currentMap.SetActive(false);
        }

        currentMap = mapToOpen;
        currentMap.SetActive(true);
        UIManager.Instance.OpenGuideBoard();
    }

    public void StartMapLevel()
    {
        playerMovement.isMovable = true;
        UIManager.Instance.CloseGuideBoard();
    }

    public void RestartGame()
    {
        playerMovement.transform.position = startPosition;
    }

    public void FinishMap()
    {
        UIManager.Instance.WinBoard.SetActive(true);
    }

    public void GoToColoringGame()
    {
        playerMovement.gameObject.SetActive(false);
        mainCamera.FocusOnPoint(coloringGamePosition);
        UIManager.Instance.ColoringGuideBoard.SetActive(true);
        UIManager.Instance.WinBoard.SetActive(false);
    }

    public void StartColoringGame()
    {
        UIManager.Instance.ColoringGuideBoard.SetActive(false);
        UIManager.Instance.ColoringGameUI.SetActive(true);
        mainCamera.ResetCamera();
    }
}
