using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private ItemSO[] items = new ItemSO[8];

    private WeaponSO equipedWeapon;
    private ArmorSO equipedArmor;
    private ConsumableSO equipedConsumable;

    public int defenceBonus { get; private set; }
    public int damageBonus { get; private set; }
}
