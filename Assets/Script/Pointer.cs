using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class Pointer : MonoBehaviour
{
    Camera cam;
    public float radius;
    Collider2D[] previousColliders;
    Collider2D[] hitColliders;
    public Vector2 circleoffset;
    Vector2 offset;
    Quaternion rotation;
    public RectTransform rect;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localPoint;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, Input.mousePosition, null, out localPoint))
        {
            direction = new Vector2( localPoint.x - transform.position.x,localPoint.y - transform.position.y);
        }
        

        transform.up = direction;
        DetectTriggers();
    }
    void DetectTriggers()
    {
        rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);
        offset = rotation * circleoffset; // Adjust the offset according to your needs

        Vector2 circleCenter = (Vector2)transform.position + offset;
        hitColliders = Physics2D.OverlapCircleAll(circleCenter, radius);
        if (previousColliders != null)
        {
            foreach (Collider2D col in previousColliders)
            {
                if (!hitColliders.Contains(col))
                {
                    col.GetComponent<Segment>().isHovered = false;
                }
            }
        }
        if(hitColliders != null)
        {
            foreach (Collider2D col in hitColliders)
            {
                col.GetComponent<Segment>().isHovered = true;
            }
        }

        previousColliders = hitColliders;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + Quaternion.Euler(0, 0, transform.eulerAngles.z) * circleoffset, radius);
    }
}
