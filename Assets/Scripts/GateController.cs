using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    private SpriteRenderer sr;
    private bool playerInRange = false;
    public GameObject activeButton;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        activeButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            activeButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            activeButton.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            if (HocULinhManager.Instance.hocULinhCompleted)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("BossScene");
            }
            else
            {
                Debug.Log("Not completed Hoc U Linh yet!");
            }
        }
    }
}
