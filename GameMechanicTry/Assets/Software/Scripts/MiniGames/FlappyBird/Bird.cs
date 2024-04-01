using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private float velocityY;
    Rigidbody2D rb;
    FBGameManager gm;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<FBGameManager>();
    }

    void Update()
    {
        if(IsJumped())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    private bool IsJumped()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Ground"))
        {
            gm.ReloadScene();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Wall"))
        {
            gm.EarnPoint();
        }
    }
}
