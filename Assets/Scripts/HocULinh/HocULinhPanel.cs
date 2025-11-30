using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HocULinhPanel : MonoBehaviour
{
    public GameObject backButton;

    public void ShowPanel()
    {
        gameObject.SetActive(true);
        HocULinhManager.Instance.LoadLinhTuButtons();
        if (GameManager.Instance.spiritsCollected < 3)
        {
            backButton.SetActive(true);
        }
        else
        {
            backButton.SetActive(false);
        }
    }
    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
