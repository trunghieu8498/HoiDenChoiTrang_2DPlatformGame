using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    private SpriteRenderer sr;

    private bool playerInRange = false;
    private bool isTriggered = false;     // true = cửa đang mở
    private bool isProcessing = false;    // true = đang xử lý, cấm bấm E

    public Animator switchAnimator;
    public List<Animator> doorAnimators = new List<Animator>();
    public GameObject activeButton;
    private AudioSource audioSource;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        activeButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isProcessing)
        {
            playerInRange = true;
            activeButton.SetActive(true);
        }
        else
        {
            activeButton.SetActive(false);
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
        if (playerInRange && Input.GetKeyDown(interactKey) && !isProcessing)
        {
            audioSource.Play();
            if (!isTriggered)
                StartCoroutine(OpenDoors());
            else
                StartCoroutine(CloseDoors());
        }
    }

    IEnumerator OpenDoors()
    {
        isProcessing = true;   // khóa bấm E
        switchAnimator.SetTrigger("Activate");
        activeButton.SetActive(false);
        isTriggered = true;

        yield return new WaitForSeconds(0.5f);

        foreach (var door in doorAnimators)
        {
            if (door != null) door.SetTrigger("Activate");
        }

        StartCoroutine(WaitForEndProcessing());
    }

    IEnumerator CloseDoors()
    {
        isProcessing = true;
        switchAnimator.SetTrigger("Deactivate");
        activeButton.SetActive(false);
        isTriggered = false;

        yield return new WaitForSeconds(0.5f);

        foreach (var door in doorAnimators)
        {
            if (door != null) door.SetTrigger("Activate");
        }

        StartCoroutine(WaitForEndProcessing());
    }

    IEnumerator WaitForEndProcessing()
    {
        yield return new WaitForSeconds(2f);
        isProcessing = false;
        if (playerInRange)
        {
            activeButton.SetActive(true);
        }
    }
}
