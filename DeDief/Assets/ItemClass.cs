using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemClass : ScriptableObject
{

    [Header("Item")] // data shared accros every item
    public string itemName;
    public Sprite itemIcon;

    public abstract ItemClass GetItem();
    public abstract WeaponClass GetWeapon();
    public abstract MiscClass GetMisc();
}
