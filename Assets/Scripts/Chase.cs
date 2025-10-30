using System.Diagnostics;
using UnityEditor.Rendering;
using UnityEngine;

public class Chase : MonoBehaviour
{
    private GameObject target;
    public float speed;
    public float aggroDistance;
    public Animator animator;
    public bool isIdle;

    private void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > aggroDistance) animator.SetBool("isIdle", true);


        else
        {
            animator.SetBool("isIdle", false);
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }
}
