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
    public static bool isMagnet = false;
    GameObject Magnet;
    public static bool useStasis = false;
    public static bool isStasis = false;
    GameObject Stasis;
    public static bool useSquareBomb = false;
    public static bool isSquareBomb = false;
    GameObject SquareBomb;
    public static bool useCircleBomb = false;
    public static bool isCircleBomb = false;
    GameObject CircleBomb;
    public static bool isEnableSkills;

    private void Awake()
    {
        Magnet = GameObject.Find("Magnet");
        Stasis = GameObject.Find("Lock");
        SquareBomb = GameObject.Find("PSquare_bomb");
        CircleBomb = GameObject.Find("PRound_bomb");
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Magnet.SetActive(false);
        Stasis.SetActive(false);
        SquareBomb.SetActive(false);
        CircleBomb.SetActive(false);
        
    }
    private void Update()
    {
        Animate_Jump();
        Animate_movement();
        FlipSprite();
        if (Input.GetMouseButtonDown(0))
        {
            isEnableSkills = false;
            SwitchAbility();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            isEnableSkills = true;
            SwitchAbility();
        }

        UpdateVisuals();
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
    void SwitchAbility()
    {
        if (isMagnet)
        {
            SetAbility(Ability.Magnet);
        }
        else if (isStasis && SkillCooldown.StasisCoolDown <= 0)
        {
            SetAbility(Ability.Stasis);
        }
        else if (isSquareBomb && SkillCooldown.SquareBombCoolDown <= 0)
        {
            SetAbility(Ability.SquareBomb);
        }
        else if (isCircleBomb && SkillCooldown.CircleBombCoolDown <= 0)
        {
            SetAbility(Ability.CircleBomb);
        }
    }

    void UpdateVisuals()
    {
        SetActive(Magnet, useMagnet);
        SetActive(Stasis, useStasis && (SkillCooldown.StasisCoolDown <= 0 || StasisPower.StasisPowerActive) && !global::Stasis.isStasis);
        SetActive(SquareBomb, useSquareBomb && SkillCooldown.SquareBombCoolDown <= 0 && BombSpawner.SquareBombCount <= 0);
        SetActive(CircleBomb, useCircleBomb && SkillCooldown.CircleBombCoolDown <= 0 && BombSpawner.CircleBombCount <= 0);
    }

    void SetActive(GameObject abilityObject, bool active)
    {
        if (abilityObject != null)
        {
            abilityObject.SetActive(active);
        }
    }

    public void ChangeAbility(string abilityName)
    {
        Ability ability;
        print(abilityName);
        if (System.Enum.TryParse(abilityName, true, out ability))
        {
            print(ability);
            SetAbility(ability);
        }
    }

    void SetAbility(Ability ability)
    {
        print(ability == Ability.Magnet && isEnableSkills);
        useMagnet = (ability == Ability.Magnet && isEnableSkills);
        useStasis = (ability == Ability.Stasis && isEnableSkills);
        useSquareBomb = (ability == Ability.SquareBomb && isEnableSkills && SkillCooldown.SquareBombCoolDown <= 0);
        useCircleBomb = (ability == Ability.CircleBomb && isEnableSkills && SkillCooldown.CircleBombCoolDown <= 0);

        isMagnet = (ability == Ability.Magnet);
        isStasis = (ability == Ability.Stasis);
        isSquareBomb = (ability == Ability.SquareBomb);
        isCircleBomb = (ability == Ability.CircleBomb);
    }

    enum Ability
    {
        Magnet,
        Stasis,
        SquareBomb,
        CircleBomb
    }
}
