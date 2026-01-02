using UnityEngine;

[System.Serializable]
public class MapData
{
    [Header("ID & State")]
    public string mapID;
    public bool defaultUnlocked;

    [Header("Map Content")]
    public GameObject mapPrefab;
    public Vector2 startMapPosition;
    public Vector2 focusPosition;

    [Header("UI")]
    public Sprite guideBoard;
    public Sprite mapNameBoard;

    [Header("Gameplay")]
    public StarPool starPool;



    public bool IsUnlocked()
    {
        return PlayerPrefs.GetInt(mapID, defaultUnlocked ? 1 : 0) == 1;
    }

    public void Unlock()
    {
        PlayerPrefs.SetInt(mapID, 1);
        PlayerPrefs.Save();
    }
}
