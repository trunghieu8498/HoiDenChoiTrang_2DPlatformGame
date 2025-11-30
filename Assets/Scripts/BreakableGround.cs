using UnityEngine;

public class BreakableGround : MonoBehaviour
{
    public float breakDelay = 3f;
    public string playerTag = "Player";

    private bool isBreaking = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(playerTag) && !isBreaking)
        {
            isBreaking = true;
            Invoke(nameof(Break), breakDelay);
        }
    }

    private void Break()
    {
        gameObject.SetActive(false);
        Invoke(nameof(Recover), 5f);
    }

    private void Recover()
    {
        gameObject.SetActive(true);
        isBreaking = false;
    }

}
