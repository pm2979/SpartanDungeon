using System;
using UnityEngine;

public enum ITEMTYPE // 아이템 타입
{
    EQUIPABLE,
    CONSUMABLE
}

public enum CONSUMABLETYPE // 소비 아이템 타입
{
    HEALTH,
    STAMINA
}

[Serializable]
public class ItemDataConsumable
{
    public CONSUMABLETYPE type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "new Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ITEMTYPE type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Equip")]
    public GameObject equipPrefab;
}
