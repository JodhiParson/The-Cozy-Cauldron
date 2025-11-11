using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Combat/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Stats")]
    public string weaponName;
    public int damage;
    public string equipAnimationBool;
}
