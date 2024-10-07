using System.Collections;
using UnityEngine;


public class PIckupObjectWithMouse : MonoBehaviour
{
    public Vector3 MousePos;
    Camera cam;
    public bool pickedUp = false;
    Rigidbody2D rb;
    public static int count = 0;
    bool moveable = true;
    float currentgravityscale;
    public float speed = 20f;
    Vector2 MoveDirection;
    LineRenderer lineRenderer;
    Transform targetObject;
    public float animationDur;
    Vector3 EndPos;
    private Vector3 pos;
    float gravityScaler;
    Material material;

    bool isRuneable;
    Transform MousePosition;


    // Start is called before the first frame update
    private void Awake()
    {
        targetObject = GameObject.Find("Magnet").transform;
    }
    void Start()
    {
        
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;     
        material = GetComponent<Renderer>().material;
        MousePosition = GameObject.Find("MouseLocation").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.isMagnet == false)
        {
            if (pickedUp)
            {
                pickedUp = false;
                rb.freezeRotation = false;
                rb.gravityScale = currentgravityscale + gravityScaler;
                lineRenderer.enabled = false;
                pos = EndPos;
                isRuneable = GetComponent<Runeable>().isRuneable = false;
                material.SetInt("_IsUseSkills", 0);
            }
            return;
        }
        isRuneable = GetComponent<Runeable>().isRuneable;
        EndPos = transform.position;
        DrawLine();
        MousePos = MousePosition.position;
        Vector3 direction = (MousePos - transform.position).normalized;
        MoveDirection = direction;
        if (isRuneable && !Stasis.isStasis)
        {
            if (Input.GetKeyDown(KeyCode.E) && !pickedUp)
            {
                currentgravityscale = rb.gravityScale;
                lineRenderer.enabled = true;
                StartCoroutine(AnimateLine());
                Timer.Register(animationDur, () => pickedUp = true);
                Timer.Register(animationDur, () => material.SetInt("_IsUseSkills", 1));
                gravityScaler = 0;


            }
            else if (Input.GetKeyDown(KeyCode.E) && pickedUp)
            {
                pickedUp = false;
                rb.freezeRotation = false;
                rb.gravityScale = currentgravityscale + gravityScaler;
                lineRenderer.enabled = false;
                pos = EndPos;
                material.SetInt("_IsUseSkills", 0);
            }

        }
        if (pickedUp)
        {
            rb.freezeRotation = true;
            if (moveable)
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                rb.velocity = new Vector2(MoveDirection.x, MoveDirection.y) * speed;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Feet"))
        {
            moveable = false;
            rb.velocity = Vector2.zero;
        }
        if (collision.gameObject.CompareTag("Body"))
        {
            if (pickedUp)
            {
                collision.transform.parent.gameObject.GetComponent<Rigidbody2D>().mass = rb.mass * 1000;
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Feet"))
        {
            moveable = false;
        }
        if (collision.gameObject.CompareTag("Body") && pickedUp)
        {
            collision.transform.parent.gameObject.GetComponent<Rigidbody2D>().mass = rb.mass * 1000;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Feet"))
        {
            moveable = true;
        }
        if (collision.gameObject.CompareTag("Body"))
        {
            collision.transform.parent.gameObject.GetComponent<Rigidbody2D>().mass = 1f;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveable = true;
        }
    }
    void DrawLine()
    {
        // Set the positions of the LineRenderer
        lineRenderer.SetPosition(0, targetObject.transform.position); // Root object position
        lineRenderer.SetPosition(1, transform.position); // Target object position
    }
    IEnumerator AnimateLine()
    {
        float startTime = Time.time;

        Vector3 StartPos = lineRenderer.GetPosition(0);

        pos = StartPos;
        while(pos != EndPos)
        {
            float t = (Time.time - startTime) / animationDur;
            pos = Vector3.Lerp(StartPos, EndPos, t);
            lineRenderer.SetPosition(1, pos);
            yield return null;
        }
    }
}
