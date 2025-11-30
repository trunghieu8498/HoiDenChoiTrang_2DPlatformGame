using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//quan li cac button linh tu
public class HocULinhManager : MonoBehaviour
{
    public static HocULinhManager Instance;
    public List<GameObject> linhTuButtons = new List<GameObject>();
    public HocULinhPanel hocULinhPanel;
    public Animator hocLinhAnimator;
    public GameObject hocULinhObject;
    public bool hocULinhCompleted = false;
    int linhTuPlaced = 0;
    int linhTuMax = 3;

    private void Awake()
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

    public void OpenHocULinhPanel()
    {
        hocULinhPanel.ShowPanel();
    }

    public void LoadLinhTuButtons()
    {
        int linhTuCount = GameManager.Instance.spiritsCollected;
        for (int i = 0; i < linhTuCount; i++)
        {
            linhTuButtons[i].GetComponent<LinhTuButton>().SetIsCollected();
            linhTuButtons[i].GetComponent<Image>().color = Color.white; //code tam trc khi co image
        }
    }

    public void PlaceLinhTuAtPosition()
    {
        linhTuPlaced++;
        Debug.Log("Linh Tu placed count: " + linhTuPlaced);
        if (linhTuPlaced >= linhTuMax)
        {
            hocLinhAnimator.SetTrigger("AllPlaced");
            Debug.Log("All Linh Tu placed");
            hocULinhObject.SetActive(false);
            hocULinhCompleted = true;
            return;
        }
    }
}
