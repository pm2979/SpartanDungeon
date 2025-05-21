using System;
using UnityEngine;
using UnityEngine.Events;

public enum ITEMTYPE // 아이템 타입
{
    EQUIPABLE,
    CONSUMABLE
}

public enum CONSUMABLETYPE // 소비 타입
{
    HEALTH,
    SPEED,
    JUMP
}

[Serializable]
public class ItemDataConsumable
{
    public CONSUMABLETYPE type;
    public float value;
    public float time;
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
    public UnityEvent active;

    [Header("Equip")]
    public GameObject equipPrefab;

}
