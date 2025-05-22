using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public Vector2 velocity;
    public float speed;
    public float rotation;
    public System.Action destroyed;

    void Start() {
        this.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
    
    void Update() {
        if (GameController.gameIsOver) return;
        transform.Translate(velocity * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Projectile(Enemy)")) {
        destroyed?.Invoke();
        Destroy(gameObject);
        }
    }
} 
