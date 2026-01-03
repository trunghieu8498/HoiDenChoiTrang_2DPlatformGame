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
    public GameObject blackPanel;

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
        OpenLobbyScreen();

        ShowTopUI();
        GuideBoard.SetActive(false);
        SelectMapScreen.SetActive(false);
        LoseBoard.SetActive(false);
        WinBoard.SetActive(false);
        MapName.SetActive(false);

        GameManager.Instance.mainCamera.onPreviewMapCompleted.AddListener(() =>
        {
            OpenGuideBoard();
            HideMapName();
            HideTopUI();
        });
    }

    public void OpenLobbyScreen()
    {
        LobbyScreen.SetActive(true);
        ShowTopUI();
        LobbyScreen.GetComponent<VideoLoader>().LoadAndPlayVideo();
    }

    public void StartMapHandle(Sprite _guideBoard)
    {
        LobbyScreen.SetActive(false);
        LoadMapUI(_guideBoard);
    }

    public void ShowTopUI()
    {
        topUI.SetActive(true);
    }
    public void HideTopUI()
    {
        topUI.SetActive(false);
    }

    public void OpenSelecMapScreen()
    {
        SelectMapScreen.SetActive(true);
        ShowTopUI();
        if (!LobbyScreen.activeSelf)
        {
            OpenLobbyScreen();
        }
    }

    public void ShowMapName()
    {
        ShowTopUI();
        MapName.SetActive(true);
    }

    public void HideMapName()
    {
        MapName.SetActive(false);
        HideTopUI();
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
        ShowBlackPanel();
        HideTopUI();
    }

    public void CloseGuideBoard()
    {
        GuideBoard.SetActive(false);
        MapName.SetActive(false);
        ShowTopUI();
        HideBlackPanel();
    }

    public void ShowLoseBoard()
    {
        LoseBoard.SetActive(true);
        ShowBlackPanel();
        HideTopUI();
    }
    public void HideLoseBoard()
    {
        LoseBoard.SetActive(false);
        HideBlackPanel();
        ShowTopUI();
    }

    public void ShowBlackPanel()
    {
        blackPanel.SetActive(true);
    }
    public void HideBlackPanel()
    {
        blackPanel.SetActive(false);
    }

    public void ShowWinBoard()
    {
        WinBoard.SetActive(true);
        ShowBlackPanel();
        HideTopUI();
    }

    public void HideWinBoard()
    {
        WinBoard.SetActive(false);
        HideBlackPanel();
        ShowTopUI();
    }
}


