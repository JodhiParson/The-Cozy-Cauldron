using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float mSpeed = 0;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
[System.Obsolete]
    void Update()
    {
        movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        rb.velocity = movement * mSpeed;
        animator.SetFloat(horizontal, movement.x);
        animator.SetFloat(vertical, movement.y);

        if (movement != Vector2.zero)
        {
            animator.SetFloat(lastHorizontal, movement.x);
            animator.SetFloat(lastVertical, movement.y);
        }
    }

}
