using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject LobbyScreen;
    public GameObject SelectMapScreen;
    public GameObject GuideBoard;
    public GameObject WinBoard;
    public GameObject LoseBoard;
    public GameObject MapName;
    public GameObject topUI;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadMapUI(Sprite guideBoard)
    {
        GuideBoard.GetComponent<UnityEngine.UI.Image>().sprite = guideBoard;
    }

    void Start()
    {
        GameManager.Instance.mainCamera.onPreviewMapCompleted.AddListener(() =>
        {
            OpenGuideBoard();
        });

        OpenLobbyScreen();

        topUI.SetActive(true);
        GuideBoard.SetActive(false);
        SelectMapScreen.SetActive(false);
        LoseBoard.SetActive(false);
        WinBoard.SetActive(false);
        MapName.SetActive(false);
    }

    public void OpenLobbyScreen()
    {
        LobbyScreen.SetActive(true);
        topUI.SetActive(true);
        LobbyScreen.GetComponent<VideoLoader>().LoadAndPlayVideo();
    }

    public void StartMapHandle(Sprite _guideBoard)
    {
        LobbyScreen.SetActive(false);
        ShowTopUI();
        LoadMapUI(_guideBoard);
    }

    public void ShowTopUI()
    {
        topUI.SetActive(true);
    }

    public void OpenSelecMapScreen()
    {
        SelectMapScreen.SetActive(true);
        topUI.SetActive(false);
        if(!LobbyScreen.activeSelf)
        {
            OpenLobbyScreen();
        }
    }

    public void ShowMapName()
    {
        MapName.SetActive(true);
    }

    public void LoadMainMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }

    public void CloseSelectMapScreen()
    {
        SelectMapScreen.SetActive(false);
    }

    public void OpenGuideBoard()
    {
        GuideBoard.SetActive(true);
    }

    public void CloseGuideBoard()
    {
        GuideBoard.SetActive(false);
        MapName.SetActive(false);
    }

    public void OpenLoseBoard()
    {
        LoseBoard.SetActive(true);
    }
    public void CloseLoseBoard()
    {
        LoseBoard.SetActive(false);
    }
}


