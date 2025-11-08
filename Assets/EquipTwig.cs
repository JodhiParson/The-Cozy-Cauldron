using UnityEngine;

public class EquipTwig : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnClick()
    {
        animator.SetBool("EquipWSword", false);
        animator.SetBool("EquipTwig", true);
    }

}
