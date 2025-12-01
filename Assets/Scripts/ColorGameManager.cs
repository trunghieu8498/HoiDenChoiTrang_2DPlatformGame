using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGameManager : MonoBehaviour
{
    public static ColorGameManager Instance { get; private set; }
    public GameObject guideColoringBoard;
    public GameObject drawBoard;
    public GameObject rewardBoard;
    public GameObject background;

    public List<GameObject> colorableObjects = new List<GameObject>();

    public GameObject finalImage;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        guideColoringBoard.SetActive(true);
        drawBoard.SetActive(false);
        rewardBoard.SetActive(false);
    }

    //active by click button
    public void StartColoringGame()
    {
        guideColoringBoard.SetActive(false);
        drawBoard.SetActive(true);
        background.SetActive(true);
    }

    public bool CheckCompletedGame()
    {
        foreach (GameObject part in colorableObjects)
        {
            if (part.activeSelf == false)
                return false;
        }

        Debug.Log("All Colored!");
        finalImage.SetActive(true);
        finalImage.GetComponent<Animator>().Play("ShowUp");
        return true;
    }

    public void EndColoringGame()
    {
        if (!CheckCompletedGame()) return;

        drawBoard.SetActive(false);
        rewardBoard.SetActive(true);
        GameManager.Instance.CompleteMapLevel();
    }
}
