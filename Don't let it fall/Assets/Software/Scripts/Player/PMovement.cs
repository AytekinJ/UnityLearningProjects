using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SampleGameManager gm;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<SampleGameManager>();
    }

    void Update()
    {
        GetInput();
        Jump();
        GravityControl();
    }

    private void FixedUpdate() {
        Movement();
    }

    #region Movement

    float horizontal;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float jumpPower = 10f;
    bool jumpButton;

    private void GetInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        jumpButton = Input.GetButtonDown("Jump");
    }

    private void Movement()
    {
        transform.position += new Vector3(horizontal * speed * Time.deltaTime, 0);
    }

    private void Jump()
    {
        if(jumpButton) rb.velocity = rb.gravityScale < 0 ? new Vector2(rb.velocity.x, -jumpPower * 2) : new Vector2(rb.velocity.x, jumpPower * 2);
    }

    private void GravityControl()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            gm.ReverseGravity();    
        }
    }

    #endregion
}
