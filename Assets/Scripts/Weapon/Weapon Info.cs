using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "Scriptable Objects/WeaponInfo")]
public class WeaponInfo : ScriptableObject
{
    public string weaponName;
    [Header("Weapon Settings")]
    [Header("Basic")]
    public int damage;
    public float range;
    public int spreadRadius;
    public int magazineSize;
    public int bulletsNumber;
    [Header("Shooting Speed")]
    public float shootingSpeed;
    public float reloadSpeed;
    public bool isAuto;
    [Header("Modificators")]
    public float speed;
    public int penetration;
    public float force;
}
