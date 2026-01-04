using UnityEngine;

public class MapData : MonoBehaviour
{
    [Header("ID & State")]
    public string mapID;
    [SerializeField]
    private bool isUnlocked = false;

    [Header("Map Content")]
    public GameObject mapPrefab;
    public Vector2 startMapPosition;
    public Vector2 focusPosition;

    [Header("UI")]
    public Sprite guideBoard;
    public Sprite mapNameBoard;

    [Header("Gameplay")]
    public StarPool starPool;
    public ColorGameManager colorGameManager;

    public PlayerMovement player;

    public float startCameraPositionX;
    public float endCameraPositionX;

    public bool IsUnlocked()
    {
        // return PlayerPrefs.GetInt(mapID, isUnlocked ? 1 : 0) == 1;
        return isUnlocked;
    }

    public void Unlock()
    {
        isUnlocked = true;
        // PlayerPrefs.SetInt(mapID, 1);
        // PlayerPrefs.Save();
    }
}
