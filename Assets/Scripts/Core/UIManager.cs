using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject SelectMapScreen;
    public GameObject GuideBoard;
    public GameObject WinBoard;
    public GameObject ColoringGuideBoard;
    public GameObject ColoringGameUI;

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

    void Start()
    {
        SelectMapScreen.SetActive(true);
    }

    public void OpenSelecMapScreen()
    {
        SelectMapScreen.SetActive(true);
        CloseGuideBoard();

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
    }
}
