using UnityEngine;

public class EquipSSword : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnClick()
    {
        animator.SetBool("EquipWSword", false);
        animator.SetBool("EquipTwig", false);
        animator.SetBool("EquipSSword", true);
    }

}
