using System.Collections;
using UnityEngine;

public class Stasis : MonoBehaviour
{
    Rigidbody2D rb;
    ParticleSystem ps;
    public GameObject ps2;
    Material material;
    public static bool isStasis = false;
    public static Vector2 Dir;
    bool isRuneable = true;
    public GameObject particlesCollision;
    TrailRenderer tr;
    float hitCount = 0;
    public GameObject Arrow;
    GameObject pointer;
    float Cooldown = 1f;
    int pcount = 0;
    // Start is called before the first frame update
    void Start()
    {
        particlesCollision.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        material = GetComponent<Renderer>().material;
        tr = GetComponent<TrailRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Player.useLock == false)
        {
            return;
        }
        isRuneable = GetComponent<Runeable>().isRuneable;
        if (isRuneable)
        {
            
            if (Input.GetKeyDown(KeyCode.E) && !isStasis && !StasisPower.BlockStasis)
            {
                pcount = 0;
                StasisPower.BlockStasis = true;
                rb.bodyType = RigidbodyType2D.Static;
                particlesCollision.SetActive(true);
                ps.Play();
                pointer = Instantiate(Arrow, transform.position, Quaternion.identity);
                material.SetInt("_IsUseSkills", 1);
                isStasis = true;
                if (isStasis)
                {
                    material.SetInt("_IsUseMagnet", 0);
                }
                StartCoroutine(StasisTime());
            }
            else if (Input.GetKeyDown(KeyCode.E) && isStasis)
            {
                StopCoroutine(StasisTime());

                rb.bodyType = RigidbodyType2D.Dynamic;
                material.SetInt("_IsUseSkills", 0);
                isStasis = false;
                EndParticles();
                AddKinematicForce(Dir, 200f);
                StartCoroutine(DelayStasis());
                tr.emitting = true;
                hitCount = 0;
                Dir = Vector2.zero;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword"))
        {
            if (isStasis)
            {
                pointer.SetActive(true);
                hitCount++;
                Dir = (transform.position - collision.transform.parent.transform.position).normalized;
            }
        } 
    }
    void AddKinematicForce(Vector2 Dir,float ForceAmount)
    {
        rb.AddForce(Dir * ForceAmount * rb.mass * hitCount,ForceMode2D.Force);
        Destroy(pointer);
        
    }
    IEnumerator StasisTime()
    {
        yield return new WaitForSeconds(5f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        material.SetInt("_IsUseSkills", 0);
        isStasis = false;
        AddKinematicForce(Dir, 200f);
        EndParticles();
        StartCoroutine(DelayStasis());
        tr.emitting = true;
        hitCount = 0;
        Dir = Vector2.zero;
    }
    void EndParticles()
    {
        pcount++;
        if (pcount > 1)
        {
            return;
        }
        var particle = Instantiate(ps2, transform.position, Quaternion.identity);
        particle.GetComponent<ParticleSystem>().Play();
        Destroy(particle, 1f);
        
    }
    IEnumerator DelayStasis()
    {
        yield return new WaitForSeconds(Cooldown);
        StasisPower.BlockStasis = false;
    }
}
