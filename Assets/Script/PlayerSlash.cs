using System.Collections;
using UnityEngine;
using UnityTimer;

public class PlayerSlash : MonoBehaviour
{
    Animator anim;
    GameObject HitBox;
    public float coolDown = 0.5f;
    bool BlockAttack = false;
    Player Player;
    int slash = 0;
    float ComboTimer = 1f;
    GameObject HitBox2;
    GameObject HitBox3;
    Rigidbody2D PlayerRb;

    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        HitBox = transform.GetChild(1).gameObject;
        HitBox.SetActive(false);
        HitBox2 = transform.GetChild(2).gameObject;
        HitBox2.SetActive(false);
        HitBox3 = transform.GetChild(3).gameObject;
        HitBox3.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        PlayerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        
        ComboTimer -= Time.deltaTime;
        if (ComboTimer <= 0)
        {
            slash = 0;
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetMouseButtonDown(0))
            {
                UpSlash();
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!Player.isGrounded)
                {
                    DownSlash();
                }
                else
                {
                    NormalSlash();
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            NormalSlash();
        }
    }
    public void Flip()
    {
        if (Player.flipped == true)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Player.flipped == false)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void UpSlash()
    {
        if (BlockAttack == true)
        {
            return;
        }
        anim.SetTrigger("SlashUp");
        BlockAttack = true;
        StartCoroutine(DelayAttact());
    }
    public void DownSlash()
    {


        if (BlockAttack == true)
        {
            return;
        }
        anim.SetTrigger("SlashDown");
        BlockAttack = true;
        StartCoroutine(DelayAttact());
    }
    public void NormalSlash()
    {
        if (BlockAttack == true)
        {
            return;
        }
        slash++;
        if (slash > 2)
        {
            slash = 1;
        }
        print(slash);
        if (slash == 1)
        {
            anim.SetTrigger("Slash");
        }
        else if (slash == 2)
        {
            anim.SetTrigger("Slash2");
        }
        BlockAttack = true;
        StartCoroutine(DelayAttact());
    }

    #region ControlHitBoxs
    public void EnableHitBox()
    {
        HitBox.SetActive(true);
    }
    public void DisableHitBox()
    {
        HitBox.SetActive(false);
    }
    public void EnableHitBox2()
    {
        HitBox2.SetActive(true);
    }
    public void DisableHitBox2()
    { 
        HitBox2.SetActive(false); 
    }
    public void EnableHitBox3()
    {
        HitBox3.SetActive(true);
    }
    public void DisableHitBox3()
    {
        HitBox3.SetActive(false);
    }
    #endregion

    public void ResetComboTimer() 
    {        
        ComboTimer = 1f;
    }
    private IEnumerator DelayAttact()
    {
        yield return new WaitForSeconds(coolDown);
        BlockAttack = false;
    }
}
