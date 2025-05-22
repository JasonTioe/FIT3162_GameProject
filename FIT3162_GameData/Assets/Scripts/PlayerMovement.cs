using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float moveSpeed = 7f;
    bool isFacingRight = true;
    float jumpPower = 7f;
    bool isJumping = false;

    Rigidbody2D rb;
    Animator animator;
    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite();
        
        if (Input.GetButtonDown("Jump") & !isJumping) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower); 
            isJumping = true;
        }
    }

    private void FixedUpdate() {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
    }

    void FlipSprite() {
        if(isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        isJumping = false;
    }
}
