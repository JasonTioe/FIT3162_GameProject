using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public Projectile projecttilePrefab;
    private bool _projectileActive;

    public int maxHealth = 5;
    public int currentHealth;
    public Healthbar healthbar;

    public GameObject gameOverUI;

    private SpriteRenderer spriteRenderer;
    public float flashDuration = 0.1f;
    public int flashCount = 5;
    private bool isInvincible = false;

    float horizontalInput;
    float moveSpeed = 7f;
    bool isFacingRight = true;
    float jumpPower = 7f;
    bool isJumping = false;

    Rigidbody2D rb;
    Animator animator;

    public AudioClip hitSound;
    public AudioClip shootSound;
    private AudioSource audioSource;

    public HealthBarController healthbarcontroller;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        gameOverUI.SetActive(false);
        GameController.gameIsOver = false;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameController.gameIsOver) return;

        horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite();

        if (Input.GetButtonDown("Jump") & !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isJumping = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameOver();
        }
        healthbarcontroller.UpdateHealthUI(currentHealth);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Projectile(Enemy)"))
        {
            if (!isInvincible)
            {
                if (hitSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(hitSound);
                }

                _projectileActive = true;
                TakeDamage(1);
                StartCoroutine(FlashBlinker());
                print(currentHealth);
                _projectileActive = false;
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            // Immediately Game Over
            GameOver();
        }
    }

    private IEnumerator FlashBlinker()
    {
        isInvincible = true;

        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashDuration);

            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashDuration);
        }

        isInvincible = false;
    }

    private void Shoot()
    {
        if (GameController.gameIsOver || _projectileActive) return;

        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        Projectile projectile = Instantiate(this.projecttilePrefab, this.transform.position, Quaternion.identity);
        projectile.destroyed += projectileDestroyed;
        _projectileActive = true;
    }

    private void projectileDestroyed()
    {
        _projectileActive = false;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
    }

    void FlipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        GameController.gameIsOver = true; // Stop everything globally
                                          // Show only Game Over UI
                                          //     foreach (GameObject obj in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None)) {
                                          //     if (obj == gameOverUI) continue;
                                          //     if (obj == this.gameObject) continue;
                                          //     if (obj.transform.parent == gameOverUI.transform) continue;
                                          //     if (obj.GetComponent<Camera>() != null) continue; // ✅ Skip cameras

        //     obj.SetActive(false);
        // }

        gameOverUI.SetActive(true);
        this.enabled = false; // Optional: disables PlayerScript
    }

}
