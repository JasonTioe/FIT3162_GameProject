using UnityEngine;

public class InvaderBullet : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // move downward
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // auto‐destroy off‐screen
        if (transform.position.y < Camera.main.ViewportToWorldPoint(Vector2.zero).y - 1f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)

    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // destroyed?.Invoke();
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}
