using UnityEngine;

public class EquipWSword : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnClick()
    {
        animator.SetBool("EquipWSword", true);
        animator.SetBool("EquipTwig", false);
    }

}
