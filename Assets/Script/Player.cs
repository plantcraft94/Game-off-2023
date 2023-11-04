using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;


    [Header("Checker")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    [SerializeField] private Vector2 groundCheckRadius;


    [Header("Gravity")]
    [SerializeField] private float gravityScale = 1f;
    [SerializeField] private float fallMultiplier = 2.5f;

    [Header("Assist")]
    [SerializeField] private float jumpBufferLength = 0.2f;
    [SerializeField] private float jumpBufferTimer;
    bool jumpBuffer;


    [SerializeField] private float cayoteTimeLength = 0.2f;


    [Header("Variables")]
    bool isGrounded;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool jumpInput;
    private float x;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {

        FlipSprite();
        x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(x * speed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, groundCheckRadius, CapsuleDirection2D.Horizontal, 0, groundLayer);
        if (isGrounded == true)
        {
            cayoteTimeLength = 0.2f;
        }
        else
        {
            cayoteTimeLength -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            jumpBuffer = true;
            jumpBufferTimer = jumpBufferLength;
            jumpInput = true;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }

        if (rb.velocity.y > 0f)
        {
            cayoteTimeLength = 0f;
        }
        else if (rb.velocity.y < 0f)
        {
            rb.gravityScale = gravityScale * fallMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -15f));
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }

    private void FixedUpdate()
    {
        // Process jump in the FixedUpdate method
        if (jumpBuffer == true)
        {
            jumpBufferTimer -= Time.deltaTime;
            if (jumpBufferTimer > 0 && (cayoteTimeLength > 0 || (isGrounded && jumpInput)))
            {
                jumpBuffer = false;
                rb.gravityScale = gravityScale;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                // Reset the jump buffer and cayote time states
                jumpInput = false;

            }
            else if (jumpBufferTimer <= 0)
            {
                jumpBuffer = false;
            }
        }

        if (jumpBuffer == false)
        {
            jumpBufferTimer = 0;
        }
    }
    private void FlipSprite()
    {
        if (x > 0)
        {
            sr.flipX = false;
        }
        else if (x < 0)
        {
            sr.flipX = true;
        }
    }
}
