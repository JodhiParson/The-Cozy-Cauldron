using UnityEngine;
using System.Collections;

public class SwordControllerPlayerDir : MonoBehaviour
{
    [Header("References")]
    public Transform player;       
    public Rigidbody2D playerRb;  

    [Header("Sword Settings")]
    public float radius = 1.5f;     
    public float swingAngle = 90f;  
    public float swingSpeed = 400f; 

    private bool swinging = false;
    private SpriteRenderer sr;
    private Vector2 lastDirection = Vector2.down; 

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
            sr = GetComponentInChildren<SpriteRenderer>();

        if (sr != null)
            sr.enabled = false; // hide sword initially
    }

    void Start()
    {
        if (sr != null)
            sr.enabled = false; // extra safety
    }

    void Update()
    {
        if (!swinging)
            SnapToPlayerDirection();

        if (Input.GetMouseButtonDown(0) && !swinging)
            StartCoroutine(Swing());
    }

    void SnapToPlayerDirection()
    {
        if (sr != null)
            sr.enabled = false;

        Vector2 moveDir = playerRb.linearVelocity.normalized;
        if (moveDir != Vector2.zero)
            lastDirection = moveDir;

        Vector3 finalDir;
        if (Mathf.Abs(lastDirection.x) > Mathf.Abs(lastDirection.y))
            finalDir = (lastDirection.x > 0) ? Vector3.right : Vector3.left;
        else
            finalDir = (lastDirection.y > 0) ? Vector3.up : Vector3.down;

        transform.position = player.position + finalDir * radius;

        float angle = 0f;
        if (finalDir == Vector3.up) angle = 90f;
        else if (finalDir == Vector3.down) angle = -90f;
        else if (finalDir == Vector3.left) angle = 180f;
        else if (finalDir == Vector3.right) angle = 0f;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator Swing()
    {
        swinging = true;
        if (sr != null) sr.enabled = true;

        // Flip the swing: startZ > endZ, subtract step instead of adding
        float startZ = transform.eulerAngles.z + swingAngle / 2f;
        float endZ = transform.eulerAngles.z - swingAngle / 2f;
        float angle = startZ;

        while (angle > endZ)
        {
            float step = swingSpeed * Time.deltaTime;
            angle -= step; // subtract instead of add
            transform.rotation = Quaternion.Euler(0, 0, angle);

            Vector3 dir;
            if (Mathf.Abs(lastDirection.x) > Mathf.Abs(lastDirection.y))
                dir = (lastDirection.x > 0) ? Vector3.right : Vector3.left;
            else
                dir = (lastDirection.y > 0) ? Vector3.up : Vector3.down;

            transform.position = player.position + dir * radius;

            yield return null;
        }

        if (sr != null) sr.enabled = false;
        swinging = false;
    }
}
