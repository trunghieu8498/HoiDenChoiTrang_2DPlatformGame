using System.Linq.Expressions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 respawnPoint = Vector2.zero;

    public void LoadStartPosition(Vector2 startPoint)
    {
        respawnPoint = startPoint;
        transform.position = startPoint;
    }
    public void SetRespawnPoint(Vector2 newPoint)
    {
        respawnPoint = newPoint;
    }

    public void Respawn()
    {
        transform.position = respawnPoint;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadLine"))
        {
            Respawn();
        }
    }
}
