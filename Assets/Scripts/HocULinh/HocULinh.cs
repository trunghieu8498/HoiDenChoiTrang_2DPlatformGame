using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HocULinh : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    private Color normalColor = new Color32(188, 188, 188, 255); //grey
    private Color activatedColor = Color.white;
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
            sr.color = activatedColor;
            activeButton.SetActive(true);
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = activatedColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            sr.color = normalColor;
            activeButton.SetActive(false);
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = normalColor;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            HocULinhManager.Instance.OpenHocULinhPanel();
        }
    }
}
