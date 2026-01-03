using System.Collections;
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
    public Vector2 endPosition;
    public GameObject currentMap;
    public StarPool starPool;

    public Vector3 coloringGamePosition = new Vector3(41, -3.6f, -10);
    public ColorGameManager coloringGame;
    // public List<MapButton> mapButtons = new List<MapButton>();
    public List<MapButtonController> mapButtons = new List<MapButtonController>();


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    public void OpenMapSelected(GameObject mapToOpen, PlayerMovement player, ColorGameManager coloringGame, Vector2 starPos, Vector2 focusPos, Sprite guideBoard, Sprite mapNameBoard, StarPool starPool)
    {
        if (currentMap != null)
        {
            currentMap.SetActive(false);
        }

        UIManager.Instance.StartMapHandle(guideBoard);

        currentMap = mapToOpen;
        currentMap.SetActive(true);
        playerMovement = player;
        player.gameObject.SetActive(true);
        starPool.ResetStars();
        playerMovement.transform.position = starPos;
        startPosition = starPos;
        UIManager.Instance.MapName.GetComponent<UnityEngine.UI.Image>().sprite = mapNameBoard;
        coloringGamePosition = new Vector3(focusPos.x, focusPos.y, -10);
        this.coloringGame = coloringGame;
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
        UIManager.Instance.HideLoseBoard();
        playerMovement.UnfreezePlayer();
    }

    public void FinishJumpGame()
    {
        UIManager.Instance.ShowWinBoard();
    }

    public void GoToColoringGame()
    {
        playerMovement.FreezePlayer();
        UIManager.Instance.HideWinBoard();
        // mainCamera.FocusOnPoint(coloringGamePosition);
        coloringGame.gameObject.SetActive(true);
        coloringGame.ResetColoringGame();
        coloringGame.SetupColoringGame();
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
            if (mapButtons[i].mapData.mapPrefab == currentMap)
            {
                if (i + 1 >= 5) break; //chi mo khoa toi da map 5
                var nextMap = mapButtons[i + 1];

                if (nextMap.mapData == null) break;

                nextMap.mapData.Unlock();

                Debug.Log("Unlocked next map!");
                break;
            }
        }
        mainCamera.ResetCamera();
    }

    public void LoadMainMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
