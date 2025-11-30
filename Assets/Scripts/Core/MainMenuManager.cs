using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; private set; }
    public Button continueButton;
    public GameObject chapterPanel;
    public GameObject settingsPanel;
    public GameObject chapterInfoPanel;
    public GameObject backToMainMenuButton;
    public ChapterButtonManager chapterButtonManager;
    public GameObject mainMenuUI;
    public GameObject loadingScreenUI;
    bool isNewGame = true;

    void Start()
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
        // kiểm tra xem có checkpoint không
        if (HasCheckpoint())
        {
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            continueButton.gameObject.SetActive(false);
        }

        mainMenuUI.SetActive(true);
        chapterPanel.SetActive(false);
        settingsPanel.SetActive(false);
        chapterInfoPanel.SetActive(false);
        loadingScreenUI.SetActive(false);
    }

    bool HasCheckpoint()
    {
        isNewGame = PlayerPrefs.HasKey("checkpoint_x") &&
               PlayerPrefs.HasKey("checkpoint_y");
        return isNewGame;
    }

    public void StartGameButtonClicked()
    {
        isNewGame = true;
        loadingScreenUI.SetActive(true);
        mainMenuUI.SetActive(false);
        // xóa checkpoint nếu có
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        ShowLoadingScreen();
    }

    // gọi khi bấm New Game
    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }


    public void ShowChapterPanel()
    {
        if (chapterPanel != null)
        {
            chapterPanel.SetActive(true);
            chapterButtonManager.ShowAllChapters();
            chapterInfoPanel.SetActive(false);
        }
    }

    public void ShowSettingsPanel()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    public void BackToMainMenu()
    {
        mainMenuUI.SetActive(true);
        if (chapterPanel != null)
        {
            chapterPanel.SetActive(false);
        }
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    public void ShowChapterInfoPanel()
    {
        chapterInfoPanel.SetActive(true);
        backToMainMenuButton.SetActive(false);
    }

    public void HideChapterInfoPanel()
    {
        chapterInfoPanel.SetActive(false);
        chapterButtonManager.ShowAllChapters();
        backToMainMenuButton.SetActive(true);
    }

    public void ShowLoadingScreen()
    {
        loadingScreenUI.SetActive(true);
        chapterInfoPanel.SetActive(false);
        chapterPanel.SetActive(false);
    }
}
