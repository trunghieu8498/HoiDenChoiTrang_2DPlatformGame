using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMap : MonoBehaviour
{
    public Vector2 newPlayerPosition;
    public bool isNextMap = true; //false is previous map

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("switch map");
            Transitor.Instance.SwitchToBlack(isNextMap, newPlayerPosition);
        }
    }
}
