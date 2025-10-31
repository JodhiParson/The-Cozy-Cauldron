using UnityEngine;

public class TwoSideChaseR : MonoBehaviour
{
    private GameObject target;
    public float speed;
    public float aggroDistance;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    private Vector2 lastMoveDir;
    private RoamLeft roamLeft;

    private const string isIdle = "isIdle";

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player");
        roamLeft = GetComponent<RoamLeft>();
        StartCoroutine(roamLeft.RoamRoutine());
    }
        private void Update()
    {
        if (target == null) return;
        
        float distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance <= aggroDistance)
        {
            Vector2 direction = (target.transform.position - transform.position).normalized;
            movement = direction;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if (movement.x != 0)
                spriteRenderer.flipX = movement.x > 0;
            animator.SetBool(isIdle, false);
            lastMoveDir = movement;
        }

        else
        {
            animator.SetBool(isIdle, true);

            movement = Vector2.zero;

            spriteRenderer.flipX = lastMoveDir.x > 0;
        }
 
    }
}
