using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMovement : MonoBehaviour
{
    float pHorizontal;
    bool groundCheck;
    public float groundCheckRadius = .35f;
    float jumpPover = 7f;
    public GameObject groundCheckPosition;
    public LayerMask groundCheckLayer;
    bool isFacingRight;

    float speed = 5f;
    Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        Flip();
        CheckSurfaceForMovement();
        Jump();
    }
    private void FixedUpdate() {
        Movement();
    }
    void Movement()
    {
        pHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(pHorizontal * speed , rb.velocity.y);
    }
    void Flip()
    {
        if(isFacingRight && pHorizontal < 0f || !isFacingRight && pHorizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    void CheckSurfaceForMovement()
    {
        groundCheck = Physics2D.OverlapCircle(groundCheckPosition.transform.position, groundCheckRadius, groundCheckLayer);
        
    }
    void Jump()
    {
        if (groundCheck == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPover);
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPosition.transform.position, groundCheckRadius);
    }
}
