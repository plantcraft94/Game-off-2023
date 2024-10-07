using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Explode : MonoBehaviour
{
    Camera mcamera;
    public float CamShakeStrength;
    public int CamVibrato;

    private void Start()
    {
        mcamera = Camera.main;
    }
    public void FinishExplode()
    {
        Destroy(gameObject);
    }
    public void BeginExplode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3.87f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == Tags.T_RemoteBomb)
            {
                collider.GetComponent<RemoteBomb>().CallExplode();
            }
            if (collider.gameObject.tag != Tags.T_ground)
            {
                collider.GetComponent<Rigidbody2D>().AddForce((collider.transform.position - transform.position) * 100f,ForceMode2D.Impulse);
            }
        }
    }
    public void cameraShake()
    {
        mcamera.DOShakePosition(1f / 6f, CamShakeStrength, CamVibrato,90f, true);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3.87f);
    }
}
