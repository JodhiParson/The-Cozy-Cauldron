using UnityEngine;

public class HurtAnim : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }
}
