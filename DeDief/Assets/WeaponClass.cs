using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new weapon class", menuName = "Item/Weapon")]
public class WeaponClass : ItemClass
{
    [Header("Weapon")]
    public WeaponType weaponType;
    public enum WeaponType
    {
        AK47,
        Pistol
    }
    public override ItemClass GetItem() { return this; }
    public override WeaponClass GetWeapon() { return this; }
    public override MiscClass GetMisc() { return null; }
}
