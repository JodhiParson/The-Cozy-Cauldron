using UnityEngine;

public class DroppedItem : MonoBehaviour, IItem
{
    public void Collect()
    {
        Destroy(gameObject);
    }
}
