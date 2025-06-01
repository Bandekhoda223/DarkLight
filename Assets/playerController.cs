using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 240f;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded = true;
    public float moveSpeed = 5f;
    private float moveInput;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            anim.SetBool("isJumping", true);
        }
        moveInput = Input.GetAxisRaw("Horizontal");

        Vector3 scale = transform.localScale;

        if (moveInput > 0)
            scale.x = Mathf.Abs(scale.x); 
        else if (moveInput < 0)
            scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;

        anim.SetBool("isWalking", moveInput != 0);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }
    }
}
