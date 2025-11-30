using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinhTuButton : MonoBehaviour
{
    public GameObject LinhTuPlacement;
    bool isCollected = false;

    public void SetIsCollected()
    {
        isCollected = true;
    }

    public void OnLinhTuButtonClick()
    {
        if (!isCollected) return;

        LinhTuPlacement.GetComponent<Image>().color = Color.yellow;
        transform.GetComponent<Image>().color = Color.black;
        HocULinhManager.Instance.PlaceLinhTuAtPosition();
    }
}
