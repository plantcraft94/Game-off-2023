using UnityEngine;

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
    public bool isGrounded;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool jumpInput;
    private float x;
    Animator anim;
    public bool flipped;

    [Header("Skill")]
    public static bool useMagnet = false;
    GameObject Magnet;
    public static bool useLock = false;
    GameObject Lock;

    private void Awake()
    {
        Magnet = GameObject.Find("Magnet");
        Lock = GameObject.Find("Lock");
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Magnet.SetActive(false);
        Lock.SetActive(false);
        
    }
    private void Update()
    {
        Animate_Jump();
        Animate_movement();
        FlipSprite();    
        Visual();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBuffer = true;
            jumpBufferTimer = jumpBufferLength;
            jumpInput = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
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
        if (sr.flipX == true)
        {
            flipped = true;
        }
        else if (sr.flipX == false)
        {
            flipped = false;
        }
    }
    private void Animate_movement()
    {
        if (rb.velocity.x != 0)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);

        }
    }
    private void Animate_Jump()
    {
        if (rb.velocity.y > 0)
        {
            anim.SetBool("Jumping", true);
            anim.SetBool("Up", true);
        }
        if (rb.velocity.y == 0 && !isGrounded)
        {
            anim.SetBool("Up", false);
        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Up", false);
        }
        if (isGrounded)
        {
            anim.SetBool("isGrounded", true);
            anim.SetBool("Up", true);
            anim.SetBool("Jumping", false);
        }
        else if (!isGrounded)
        {
            anim.SetBool("isGrounded", false);
        }

    }
    void Visual()
    {
        if (useMagnet == true)
        {
            Magnet.SetActive(true);
        }
        else if (useMagnet == false)
        {
            Magnet.SetActive(false);
        }
        if (useLock == true)
        {
            Lock.SetActive(true);
        }
        else if (useLock == false)
        {
            Lock.SetActive(false);
        }
    }
    public void ChangeAbility(string AbilityName)
    {
        if (AbilityName == "Magnet")
        {
            useMagnet = true;
            useLock = false;
        }
        if (AbilityName == "Stasis")
        {
            useLock = true;
            useMagnet = false;
        }
    }
}
