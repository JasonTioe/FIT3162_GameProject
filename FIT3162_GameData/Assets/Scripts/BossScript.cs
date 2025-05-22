using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    private Vector3 _direction = Vector2.right;
    public float speed = 10.0f;
    public int maxHealth = 5;
    public int currentHealth;
    public Healthbar healthbar;


    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }


    void TakeDamage(int damage) {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Projectile(Player)")) {
            TakeDamage(1);
            print(currentHealth);
        }
    }
}
