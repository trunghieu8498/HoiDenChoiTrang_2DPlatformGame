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
    public GameObject currentMap;

    public Vector3 coloringGamePosition = new Vector3(41, -3.6f, -10);
    public GameObject coloringGame;
    public List<MapButton> mapButtons = new List<MapButton>();


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


    public void OpenMapSelected(GameObject mapToOpen, PlayerMovement player, GameObject coloringGame, Vector2 starPos, Vector2 focusPos, Sprite guideBoard)
    {
        if (currentMap != null)
        {
            currentMap.SetActive(false);
        }
        currentMap = mapToOpen;
        currentMap.SetActive(true);
        playerMovement = player;
        player.gameObject.SetActive(true);
        playerMovement.transform.position = starPos;
        startPosition = starPos;

        coloringGamePosition = new Vector3(focusPos.x, focusPos.y, -10);
        this.coloringGame = coloringGame;

        UIManager.Instance.LoadMapUI(guideBoard);
        UIManager.Instance.OpenGuideBoard();
    }

    public void StartMapLevel()
    {
        playerMovement.isMovable = true;
        mainCamera.FollowPlayer(playerMovement.transform);
        UIManager.Instance.CloseGuideBoard();
    }

    public void RestartGame()
    {
        playerMovement.transform.position = startPosition;
    }

    public void FinishJumpGame()
    {
        UIManager.Instance.WinBoard.SetActive(true);
    }

    public void GoToColoringGame()
    {
        playerMovement.gameObject.SetActive(false);
        UIManager.Instance.WinBoard.SetActive(false);
        mainCamera.FocusOnPoint(coloringGamePosition);
        coloringGame.SetActive(true);
    }

    public void CompleteMapLevel()
    {
        UnlockNextMap();
        mainCamera.ResetCamera();
    }

    public void UnlockNextMap()
    {
        //check xem currentmap thuoc map button nao de mo khoa map ke tiep
        for (int i = 0; i < mapButtons.Count - 1; i++)
        {
            if (mapButtons[i].mapToOpen == currentMap)
            {
                mapButtons[i + 1].isUnlocked = true;
                Debug.Log("Unlocked next map!");
                break;
            }
        }
        mainCamera.ResetCamera();
    }


}
