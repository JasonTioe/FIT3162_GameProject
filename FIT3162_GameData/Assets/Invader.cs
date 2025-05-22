using UnityEngine;

public class Invader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Make sure this is your player’s shot
        if (other.gameObject.layer == LayerMask.NameToLayer("Projectile(Player)"))
        {
            Destroy(other.gameObject);   // remove the bullet
            Destroy(this.gameObject);    // remove the invader
        }
    }
}
