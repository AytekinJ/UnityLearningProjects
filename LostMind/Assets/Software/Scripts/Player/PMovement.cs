using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMovement : MonoBehaviour
{
    #region Movement
    [Header("Movement")]
    [SerializeField] private float speed = 10;
    [SerializeField] private float horizontalInput;

    [Header("Jump")]
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private bool isGrounding;
    [SerializeField] private float surfaceRadius = 0.25f;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private bool coyoteUsed;
    [SerializeField] private Transform SurfacePosition;
    [SerializeField] private LayerMask GroundLayer;

    [Header("Wall Jump Slide")]
    [SerializeField] private float wallSlidingSpeed = 2;
    [SerializeField] private float wallCheckRadius = 0.2f;
    [SerializeField] private int wallJumpLeft = 1;
    [SerializeField] private Transform WallCheckPosition;
    [SerializeField] private LayerMask WallLayer;
    private bool isWallSliding;
    private bool isFacingRight = true;
    
    #endregion

    #region Inputs
    [Header("Inputs")]
    [SerializeField] KeyCode JumpInput = KeyCode.Space;
    #endregion

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        Flip();
        Jump();
        WallSlide();
    }

    private void FixedUpdate() {
        Movement();
    }

    private void Jump()
    {
        if(IsGorunded())
        {
            coyoteTimeCounter = coyoteTime;
            isGrounding = true;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            isGrounding = false;
        }
        

        if(Input.GetKeyDown(JumpInput) && coyoteTimeCounter > 0) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        if(Input.GetKeyUp(JumpInput) && rb.velocity.y > 0) 
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0;
        }
        
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(WallCheckPosition.position, wallCheckRadius, WallLayer);
    }

    private bool IsGorunded()
    {
        return Physics2D.OverlapCircle(SurfacePosition.position, surfaceRadius, GroundLayer);
    }

    private void WallSlide()
    {
        if(IsWalled() && !IsGorunded() && horizontalInput != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void Movement()
    {
        rb.velocity = new Vector2(horizontalInput * 20 * speed * Time.deltaTime, rb.velocity.y);
    }

    private void Flip()
    {
        if(isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(SurfacePosition.position, surfaceRadius);
    }

    IEnumerator CoyoteTimer()
    {
        yield return new WaitForSeconds(1f);
        coyoteTime = 0.2f;
    }

}
