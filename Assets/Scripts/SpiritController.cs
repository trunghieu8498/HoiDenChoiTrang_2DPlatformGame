using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritController : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance.IsSpiritCollected(gameObject.name))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource source = GetComponent<AudioSource>();
            source.Play();

            GameManager.Instance.CollectSpirit(gameObject.name);

            // Ẩn object nhặt đi
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // Xóa object sau khi âm thanh phát xong
            Destroy(gameObject, source.clip.length);
        }
    }

}
