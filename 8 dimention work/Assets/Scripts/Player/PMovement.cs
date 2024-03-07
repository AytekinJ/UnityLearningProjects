using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PMovement : MonoBehaviour
{
    #region Movement Variables
    Vector3 movement;
    private enum State{
        Normal,
        Rolling,
    }
    
    public float idleDirectionX;
    public float idleDirectionY;
    public float speed = 100f;

    

    Vector3 rollDirection;
    float dodgeRollTimer;
    float rollSpeed;

    #endregion

    PlayerControls controls;
    private State state;

    Rigidbody2D rb;
    Animator animator;

    PAttack attack;

    void Awake() {
        controls = new PlayerControls();
        
        if(controls.FindAction("Gameplay") != null)
        {
            print("True");
        }

        attack = GetComponent<PAttack>();
    }
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        state = State.Normal;
    }

    private void FixedUpdate() {
        Movement();
        AnimatorVariables();
        
    }
    private void Update() {
        DodgeRoll();
    }
    void Movement()
    {
        switch(state){
            
            case State.Normal:

                if(attack.comboCounter == 0)
                {
                    movement.x = Input.GetAxisRaw("Horizontal");
                    movement.y = Input.GetAxisRaw("Vertical");
                    movement.Normalize();
                    rb.velocity = new Vector2(movement.x * speed * Time.deltaTime, movement.y * speed * Time.deltaTime);
                }
                else
                {
                    rb.velocity = new Vector2(0, 0);
                }

                if(movement.x != 0 || movement.y != 0)
                {
                    idleDirectionX = movement.x;
                    idleDirectionY = movement.y;
                }
            break;

            case State.Rolling:

            float rollSpeedDropMultiplier = 5f;
            rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;

            float rollspeedMinimum = 3f;
            if(rollSpeed < rollspeedMinimum)
            {
                state = State.Normal;
            }

            rb.velocity = rollDirection * rollSpeed;
            break;
        }
        
    }

    void DodgeRoll()
    {
        if(Input.GetKeyDown(KeyCode.Space) && dodgeRollTimer <= 0 && Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y) != 0 && attack.comboCounter == 0)
        {
            rollDirection = movement;
            animator.SetFloat("RollDirectionX", movement.x);
            animator.SetFloat("RollDirectionY", movement.y);
            animator.SetTrigger("Roll");
            rollSpeed = 15f;
            state = State.Rolling;
            dodgeRollTimer = 0.5f;
            
        }

        if(dodgeRollTimer >= 0)
        {
            dodgeRollTimer -= Time.deltaTime;
        }
        
    }

    void AnimatorVariables()
    {
        animator.SetFloat("VelocityX", rb.velocity.x);
        animator.SetFloat("VelocityY", rb.velocity.y);
        animator.SetFloat("Velocity", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));
        animator.SetFloat("IdleHorizontal", idleDirectionX);
        animator.SetFloat("IdleVertical", idleDirectionY);
    }
}
