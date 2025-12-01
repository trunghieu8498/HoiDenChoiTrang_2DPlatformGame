using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            DontDestroyOnLoad(gameObject);
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

        LobbyScreen.SetActive(true);
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
    }

    public void ShowTopUI()
    {
        topUI.SetActive(true);
    }

    public void OpenSelecMapScreen()
    {
        SelectMapScreen.SetActive(true);
        topUI.SetActive(false);
        CloseGuideBoard();
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


