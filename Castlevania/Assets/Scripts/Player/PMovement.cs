using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PMovement : MonoBehaviour
{
    #region Movement Variables
    float pHorizontal;
    public bool groundCheck;
    bool isFacingRight = true;
    public bool isCrouched;
    public float groundCheckRadius = .35f;
    float jumpPover = 6f;
    public GameObject groundCheckPosition;
    public LayerMask groundCheckLayer;
    public LayerMask enemyCheckLayer;
    #endregion
    int comboCount;
    float speed = 150f;
    bool attackControl;
    public Transform AttakingPoint;
    Rigidbody2D rb;
    Animator animator;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckSurfaceForMovement4();
        Jump();
        Flip();
        Crouch();
        attackControl = GetComponent<PAttack>().isAttacking;
    }
    private void FixedUpdate() {
        Movement();
    }

    #region Movement Codes
    void Movement()
    {
        comboCount = GetComponent<PAttack>().comboCounter;
        if(!isCrouched && comboCount == 0)
        {
            pHorizontal = Input.GetAxis("Horizontal");
        }
        else
        {
            if(groundCheck)
            {
                pHorizontal = 0;
            }
           
        }
        rb.velocity = new Vector2(pHorizontal * speed * Time.deltaTime , rb.velocity.y);
        
        animator.SetFloat("VerticalVel", rb.velocity.y);
        animator.SetFloat("Horizontal", Math.Abs(pHorizontal));

        
        
    }

    void Flip()
    {
        if(isFacingRight && pHorizontal < 0f || !isFacingRight && pHorizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            //intractObbject.gameObject.GetComponent<SpriteRenderer>().flipX = !intractObbject.gameObject.GetComponent<SpriteRenderer>().flipX;
            transform.localScale = localScale;
        }
    }
    void CheckSurfaceForMovement4()
    {
        groundCheck = Physics2D.OverlapCircle(groundCheckPosition.transform.position, groundCheckRadius, groundCheckLayer);
        animator.SetBool("isGrounded", groundCheck);
        
    }
    void Crouch()
    {
        if(groundCheck)
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                isCrouched = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                AttakingPoint.position = new Vector3(transform.position.x + 0.85f, transform.position.y + 0.5f);
            }
            
            if(Input.GetKeyUp(KeyCode.C))
            {
                isCrouched = false;
                rb.constraints =  RigidbodyConstraints2D.FreezeRotation;
                AttakingPoint.position = new Vector3(transform.position.x + 0.85f, transform.position.y + 0.825f);
            }
        }
        animator.SetBool("isCrouched", isCrouched);
    }
    void Jump()
    {
        if (groundCheck && !isCrouched && attackControl == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPover);
            }
        }

        if(groundCheck == false)
        {
            speed = 110f;
        }
        else
        {
            speed = 150f;
        }
    }
    #endregion

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPosition.transform.position, groundCheckRadius);
    }
}
