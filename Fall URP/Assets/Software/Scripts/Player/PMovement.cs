using UnityEngine;

public class PMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float horizontalInput;
    float speed = 8f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        horizontalInput = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.gravityScale = 3f;
        }
        
        if(Input.GetKeyUp(KeyCode.Space))
        {
            rb.gravityScale = 1f;
        }

        

    }
    private void FixedUpdate() {
        Movement();
    }

    void Movement()
    {
        if(horizontalInput != 0)
        {
            transform.position += new Vector3(horizontalInput * speed * Time.deltaTime, 0);
        }
    }
}
