using UnityEngine;

public class DeleteArrowWhenTwigPickedUp : MonoBehaviour
{
    [Header("Assign the Twig GameObject here")]
    public GameObject twig;

    private void Update()
    {
        // If the twig was destroyed (picked up), delete the arrow
        if (twig == null)
        {
            Destroy(gameObject); // delete the arrow
        }
    }
}
