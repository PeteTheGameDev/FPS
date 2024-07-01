using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Weapon/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Tooltip("Name of weapon")]
    public new string name;

    [Tooltip("Damage per shot")]
    public float bulletDamage;

    [Tooltip("Minimum damage range that the weapon can deal the maximum amount of damage per shot")]
    public float maxDamageRange;

    [Tooltip("Minimum damage range that the weapon can deal the minimum amount of damage per shot")]
    public float minDamageRange;

    [Tooltip("How much RPM the weapon fires at")]
    public float fireRate;

    // Need to implement floats for vertical and horizontal recoil

    [Tooltip("Maximum bullet spread when firing")]
    public float maxBulletSpread;

    [Tooltip("Minimum bullet spread when firing")]
    public float minBulletSpread;

    [Tooltip("How long it takes for the weapon to reload")]
    public float reloadTime;

    [HideInInspector]
    public bool isReloading;

    public bool isFullAuto;
    public bool isSemiAuto;
    public bool isBurst;

    [Tooltip("Amount of bullet per mag/clip")]
    public int magSize;

    [Tooltip("Amount of ammo player can have in order to reload")]
    public int maxAmmoAmount;

    [Tooltip("Current ammo in weapon mag/clip")]
    public int currentAmmoAmount;
}
