using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject SelectMapScreen;
    public GameObject GuideBoard;
    public GameObject WinBoard;
    public GameObject LoseBoard;

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

    public void OpenLoseBoard()
    {
        LoseBoard.SetActive(true);
    }
    public void CloseLoseBoard()
    {
        LoseBoard.SetActive(false);
    }
}

    
