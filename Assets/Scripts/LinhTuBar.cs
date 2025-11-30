using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinhTuBar : MonoBehaviour
{
    public List<GameObject> linhTus = new List<GameObject>();

    public void SetLinhTu()
    {
        for (int i = 0; i < linhTus.Count; i++)
        {
            if (i < GameManager.Instance.spiritsCollected)
            {
                linhTus[i].SetActive(true);
                linhTus[i].GetComponent<Animator>().SetTrigger("Activate");
            }
            else
            {
                linhTus[i].SetActive(false);
            }
        }
    }

}
