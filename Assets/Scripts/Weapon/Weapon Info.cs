using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "Scriptable Objects/WeaponInfo")]
public class WeaponInfo : ScriptableObject
{
    public string weaponName;
    [Header("Weapon Settings")]
    public int damage;
    public int bulletsNumber;
    public float reloadTime;
    public float range;
    public float speed;
    public bool isAuto;
}
