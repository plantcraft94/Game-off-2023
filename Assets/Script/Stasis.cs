using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stasis : MonoBehaviour
{
    Rigidbody2D rb;
    ParticleSystem ps;
    public GameObject ps2;
    Material material;
    public static bool isStasis = false;
    Vector2 Dir;
    bool isRuneable = true;
    public GameObject particlesCollision;
    TrailRenderer tr;
    int hitCount = 0;

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
            print("Runeable");
            if (Input.GetKeyDown(KeyCode.E))
            {
                rb.bodyType = RigidbodyType2D.Static;
                isStasis = true;
                particlesCollision.SetActive(true);
                ps.Play();
                material.SetInt("_IsUseSkills", 1);
                if (isStasis)
                {
                    material.SetInt("_IsUseMagnet", 0);
                }
                StartCoroutine(StasisTime());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword"))
        {
            if (isStasis)
            {
                hitCount++;
                Dir = (transform.position - collision.transform.parent.transform.position).normalized;
            }  
        } 
    }
    void AddKinematicForce(Vector2 Dir,float ForceAmount)
    {
        rb.AddForce(Dir * ForceAmount * rb.mass,ForceMode2D.Impulse);
    }
    IEnumerator StasisTime()
    {
        yield return new WaitForSeconds(5f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        material.SetInt("_IsUseSkills", 0);
        isStasis = false;
        EndParticles();
        AddKinematicForce(Dir, 1000);
        Dir = Vector2.zero;
    }
    void EndParticles()
    {
        var particle = Instantiate(ps2, transform.position, Quaternion.identity);
        particle.GetComponent<ParticleSystem>().Play();
        Destroy(particle, 1f);
        
    }
}
