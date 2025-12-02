using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; private set; }
    public GameObject mainMenuUI;
    public VideoLoader loadingScreen;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        mainMenuUI.SetActive(true);
        loadingScreen.onVideoEnd.AddListener(() =>
        {
            LoadGameScene();
        });
    }

    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void ShowLoadingScreen()
    {
        loadingScreen.gameObject.SetActive(true);
        loadingScreen.LoadAndPlayVideo();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
