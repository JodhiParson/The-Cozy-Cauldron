using NUnit.Framework;
using UnityEngine;
using System.Collections;

public class EnemyOnHit : MonoBehaviour
{
    public float duration = 3f;
    public float speedMul;
    private RoamLeft roamLeft;
    private Animator animator;
    private bool isFrantic = false;
    private float normalMoveSpeed;
    private float normalAnimSpeed;

    void Start()
    {
        roamLeft = GetComponent<RoamLeft>();
        animator = GetComponent<Animator>();

        if (roamLeft == null)
        {
            normalMoveSpeed = roamLeft.roamSpeed;
        }
        if (animator == null)
        {
            normalAnimSpeed = animator.speed;
        }
        Health health = GetComponent<Health>();
        if (health == null)
        {
            health.OnHitWithReference.AddListener(OnHit);
        }
    }

    private void OnHit(GameObject attacker)
    {
        if (!isFrantic)
            StartCoroutine(FranticRoutine());
    }

    private IEnumerator FranticRoutine()
    {
        isFrantic = true;
        if (roamLeft != null)
            roamLeft.roamSpeed = normalMoveSpeed * speedMul;
        if (animator != null)
            animator.speed = normalAnimSpeed * speedMul;

        yield return new WaitForSeconds(duration);

        if (roamLeft != null)
            roamLeft.roamSpeed = normalMoveSpeed;
        if (animator != null)
            animator.speed = normalAnimSpeed;

        isFrantic = false;
    }
}
