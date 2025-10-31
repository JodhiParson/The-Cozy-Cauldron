using UnityEngine;
using System.Collections;

public class RoamLeft : MonoBehaviour
{
    public float roamRadius = 10f;        // How far from the start position it can move
    public float roamSpeed = 6f;       // Movement speed while roaming
    public float waitTime = 2f;          // Time to wait before picking a new target
    public float changeDirectionChance = 0.3f; // Chance to pick a new random direction before reaching destination

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private bool isRoaming = true;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private const string IS_IDLE = "isIdle";

    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

        StartCoroutine(RoamRoutine());
    }

    public IEnumerator RoamRoutine()
    {
        while (isRoaming)
        {
            PickNewDestination();
            animator.SetBool(IS_IDLE, false);

            // Move until close to target
            while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
            {
                MoveTowards(targetPosition);
                yield return null;

                // Randomly change direction sometimes
                if (Random.value < changeDirectionChance * Time.deltaTime)
                    PickNewDestination();
            }

            animator.SetBool(IS_IDLE, true);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void PickNewDestination()
    {
        Vector2 randomOffset = Random.insideUnitCircle * roamRadius;
        targetPosition = startPosition + randomOffset;
    }

    void MoveTowards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, target, roamSpeed * Time.deltaTime);

        // Flip sprite based on direction
        if (spriteRenderer != null && Mathf.Abs(direction.x) > 0.05f)
            spriteRenderer.flipX = direction.x > 0;
    }

    public void EnableRoam(bool enable)
    {
        isRoaming = enable;
        if (enable)
            StartCoroutine(RoamRoutine());
        else
            StopAllCoroutines();
    }
}
