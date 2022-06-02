using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] protected string title;
    [SerializeField] protected Sprite sprite;

    [SerializeField] protected Rarity rarity;

    [SerializeField] protected ItemType type;
    [SerializeField] protected Stat riseStat;

    [SerializeField] protected int riseValue;

    public string Title { get { return title; } }
    public Sprite Sprite { get { return sprite; } }
    public ItemType Type { get { return type; } }
    public int RiseValue { get { return riseValue; } }

    public virtual Item CreateInstance() => new Item(title, rarity, type, riseStat, riseValue, sprite);
}
