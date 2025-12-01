using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    public CameraMovement mainCamera;
    public PlayerMovement playerMovement;
    public Vector2 startPosition;
    public Vector2 endPosition;
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


    public void OpenMapSelected(GameObject mapToOpen, PlayerMovement player, GameObject coloringGame, Vector2 starPos, Vector2 focusPos, Sprite guideBoard, Sprite mapNameBoard)
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
        UIManager.Instance.MapName.GetComponent<UnityEngine.UI.Image>().sprite = mapNameBoard;
        coloringGamePosition = new Vector3(focusPos.x, focusPos.y, -10);
        this.coloringGame = coloringGame;
        UIManager.Instance.ShowTopUI();
        UIManager.Instance.LoadMapUI(guideBoard);
        PreviewMapLevel(starPos, endPosition);
    }

    public void PreviewMapLevel(Vector2 startPos, Vector2 endPos)
    {
        mainCamera.ResetCamera();
        playerMovement.FreezePlayer();
        UIManager.Instance.ShowMapName();
        StartCoroutine(WaitBeforePreview(2f));
    }

    public void StartMapLevel()
    {
        playerMovement.UnfreezePlayer();
        mainCamera.FollowPlayer(playerMovement.transform);
        UIManager.Instance.CloseGuideBoard();
    }

    IEnumerator WaitBeforePreview(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        mainCamera.MoveFromTo(new Vector3(0, 0, -10), new Vector3(34, 0, -10), 2f);
    }

    public void RestartGame()
    {
        playerMovement.transform.position = startPosition;
        UIManager.Instance.CloseLoseBoard();
        playerMovement.UnfreezePlayer();
    }

    public void FinishJumpGame()
    {
        UIManager.Instance.WinBoard.SetActive(true);
    }

    public void GoToColoringGame()
    {
        playerMovement.FreezePlayer();
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
                if (i + 1 >= 2) break; //chi mo khoa toi da map 2
                mapButtons[i + 1].isUnlocked = true;
                Debug.Log("Unlocked next map!");
                break;
            }
        }
        mainCamera.ResetCamera();
    }


}
