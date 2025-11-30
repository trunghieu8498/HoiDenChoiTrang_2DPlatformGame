using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Gameplay")]
    public int spiritsCollected = 0;
    public bool isNewGame = true;

    [Header("References")]
    public Camera mainCamera;
    public PlayerController playerController;
    public LinhTuBar linhTuBar;

    [Header("Maps")]
    public List<GameObject> maps = new List<GameObject>();
    public Vector2 startPosition;

    private int currentMapIndex = 0;
    private readonly HashSet<string> collectedSpiritNames = new HashSet<string>();
    public GameEvent onGuideGame;

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
        if (CheckSaveExists())
        {
            isNewGame = false;
            LoadSavedGame();
        }
        else
        {
            isNewGame = true;
            Debug.Log("Starting new game");
            StartNewGame();
        } 
    }

    private void StartNewGame()
    {
        currentMapIndex = 0;
        spiritsCollected = 0;
        collectedSpiritNames.Clear();

        LoadMaps();
        playerController.LoadStartPosition(startPosition);
        linhTuBar.SetLinhTu();

        // Không xóa PlayerPrefs toàn bộ nữa, chỉ xóa những gì liên quan đến game hiện tại nếu cần
        PlayerPrefs.DeleteKey("map_index");
        PlayerPrefs.DeleteKey("checkpoint_x");
        PlayerPrefs.DeleteKey("checkpoint_y");
        PlayerPrefs.DeleteKey("linhtu_count");
        PlayerPrefs.DeleteKey("collected_spirits");

        PlayerPrefs.Save();
    }

    public bool CheckSaveExists()
    {
        return PlayerPrefs.HasKey("map_index");
    }

    public void CollectSpirit(string spiritName)
    {
        if (collectedSpiritNames.Add(spiritName))
        {
            spiritsCollected++;
            linhTuBar.SetLinhTu();
            SaveCollectedSpirits();
        }
    }

    public bool IsSpiritCollected(string spiritName)
    {
        return collectedSpiritNames.Contains(spiritName);
    }

    public void SaveCheckpoint(Vector2 playerPos)
    {
        PlayerPrefs.SetFloat("checkpoint_x", playerPos.x);
        PlayerPrefs.SetFloat("checkpoint_y", playerPos.y);

        PlayerPrefs.SetFloat("camera_x", mainCamera.transform.position.x);
        PlayerPrefs.SetFloat("camera_y", mainCamera.transform.position.y);

        PlayerPrefs.SetInt("map_index", currentMapIndex);
        PlayerPrefs.SetInt("linhtu_count", spiritsCollected);

        SaveCollectedSpirits();

        PlayerPrefs.Save();
    }


    private void SaveCollectedSpirits()
    {
        PlayerPrefs.SetString("collected_spirits", string.Join(",", collectedSpiritNames));
    }

    private void LoadCollectedSpirits()
    {
        collectedSpiritNames.Clear();
        string saved = PlayerPrefs.GetString("collected_spirits", "");
        if (saved.Length == 0) return;

        foreach (string s in saved.Split(','))
            collectedSpiritNames.Add(s);
    }


    private void LoadSavedGame()
    {
        currentMapIndex = PlayerPrefs.GetInt("map_index", 0);
        LoadMaps();

        playerController.LoadStartPosition(LoadVector("checkpoint_x", "checkpoint_y", playerController.transform.position));
        Vector2 camPos = LoadVector("camera_x", "camera_y", mainCamera.transform.position);
        mainCamera.transform.position = new Vector3(camPos.x, camPos.y, mainCamera.transform.position.z);

        spiritsCollected = PlayerPrefs.GetInt("linhtu_count", 0);
        linhTuBar.SetLinhTu();

        LoadCollectedSpirits();

        Debug.Log("Loaded saved game at map " + currentMapIndex + " with " + spiritsCollected + " spirits.");
    }


    public Vector2 LoadVector(string keyX, string keyY, Vector2 fallback)
    {
        float x = PlayerPrefs.GetFloat(keyX, fallback.x);
        float y = PlayerPrefs.GetFloat(keyY, fallback.y);
        return new Vector2(x, y);
    }

    public Vector2 LoadCamera(Vector2 fallback)
    {
        return LoadVector("camera_x", "camera_y", fallback);
    }



    private void LoadMaps()
    {
        for (int i = 0; i < maps.Count; i++)
            maps[i].SetActive(i == currentMapIndex);

        playerController.transform.SetParent(maps[currentMapIndex].transform);
    }


    public void NextMap()
    {
        currentMapIndex++;
        if (currentMapIndex >= maps.Count)
        {
            currentMapIndex = maps.Count - 1;
            Debug.Log("Completed game!");
            return;
        }
        LoadMaps();
    }

    public void PreviousMap()
    {
        currentMapIndex = Mathf.Max(0, currentMapIndex - 1);
        LoadMaps();
    }
}
