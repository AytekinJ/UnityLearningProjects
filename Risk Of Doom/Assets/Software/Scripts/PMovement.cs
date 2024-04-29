using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PMovement : MonoBehaviour
{

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        Movement();
        Gravity();
        GroundCheck();
    }


    #region Movement And Gravity
    [Header("Movement")]
    [SerializeField]
    private float speed = 10f;

    [Header("Gravity")]
    [SerializeField]
    private float mass = 5f;

    [SerializeField]
    private float gravityForce = -9.81f;
    
    [SerializeField]
    private bool isGrounded;

    [SerializeField]
    [Range(0f, 1f)]
    private float castRadius = 0.5f;
    [SerializeField]
    [Range(0f, 1f)]
    private float castDistance = 0.6f;

    [SerializeField]
    private Transform groundTransform;

    [SerializeField]
    private LayerMask groundLayer;

    CharacterController _controller;
    float _horizontal;
    float _vertical;
    [SerializeField]
    float _yVelocity;

    void Movement()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(_horizontal, 0f, _vertical);
        movement = Vector3.ClampMagnitude(movement, 1f);

        Vector3 moveDirection = transform.TransformDirection(movement);
        _controller.Move(moveDirection * speed * Time.deltaTime);

        // _controller.Move(new Vector3(_horizontal * speed * Time.deltaTime, 0, _vertical * speed * Time.deltaTime));
    }
    
    void Gravity()
    {
        if (!isGrounded)
        {
            _yVelocity += gravityForce * 0.5f * Time.deltaTime;
        }
        else
        {
            _yVelocity = 0f;
        }

        Vector3 velocity = new Vector3(0, _yVelocity, 0);
        _controller.Move(velocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        isGrounded = Physics.OverlapSphere(groundTransform.position, castRadius, groundLayer).Length > 0;
    }

    #endregion


    #region Others
    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(groundTransform.position, castRadius);
    }
    #endregion

}
