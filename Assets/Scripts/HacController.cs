using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacController : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    private bool playerInRange = false;
    private bool isTriggered = false;
    public GameObject Ebutton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            playerInRange = true;
            Ebutton.SetActive(true);
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey) && !isTriggered)
        {
            isTriggered = true;
            GameManager.Instance.onGuideGame.Raise();
            Ebutton.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            isTriggered = false;
            Ebutton.SetActive(false);
        }
    }
}
