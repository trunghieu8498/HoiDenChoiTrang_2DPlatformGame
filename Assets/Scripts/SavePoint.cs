using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().SetRespawnPoint(transform.position);
            Debug.Log("Checkpoint saved at: " + transform.position);
            GetComponent<Animator>().SetTrigger("Activate");
            GameManager.Instance.SaveCheckpoint(transform.position);
        }
    }
}
