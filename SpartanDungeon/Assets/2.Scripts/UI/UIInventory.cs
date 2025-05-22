using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public ItemSlot equipSlot;

    public Transform slotPanel;
    public Transform dropPosition;

    void Start()
    {
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
        }
    }

    private void OnEnable()
    {
        EventBus.Subscribe("AddItem", AddItem);
        EventBus.Subscribe("UseItem", ItemActive);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe("AddItem", AddItem);
        EventBus.Unsubscribe("UseItem", ItemActive);
    }

    private void AddItem(object data) // 아이템을 얻었을 때
    {
        var _data = (ItemData) data;
        // 비어있는 슬롯을 가져온다.

        if(_data.type == ITEMTYPE.EQUIPABLE)
        {
            AddEquipable(_data);
        }
        else
        {
            AddConsumable(_data);
        }
    }

    private void AddEquipable(ItemData data) // 장비 아이템 획득 시
    {
        if (equipSlot.item != null) // 기존 장비 버리기
        {
            ThrowItem(equipSlot.item);
        }

        equipSlot.item = data;
        UpdateEquipUI();
    }

    private void AddConsumable(ItemData data) // 소비 아이템 획득 시
    {
        ItemSlot emptySlot = GetEmptySlot();

        // 있다면
        if (emptySlot != null)
        {
            emptySlot.item = data;
            UpdateConsumUI();

            return;
        }

        ThrowItem(data);
    }

    private void UpdateConsumUI() // 소비 아이템 UI
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    private void UpdateEquipUI() // 장비 아이템 UI
    {
        if (equipSlot != null)
        {
            equipSlot.Set();
        }
        else
        {
            equipSlot.Clear();
        }
    }

    private ItemSlot GetEmptySlot() // 비어있는 슬롯을 가져온다.
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null) // 슬롯이 비어있으면 return
            {
                return slots[i];
            }
        }
        return null;
    }

    private void ThrowItem(ItemData data) // 아이템 버리기
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    private void ItemActive(object evt) // 아이템 사용
    {
        if (slots[0].item != null)
        {
            slots[0].item.active?.Invoke();

            RemoveConsumableItem();
        }
    }

    private void RemoveConsumableItem() // 아이템 사용시 뒤에 아이템 앞으로
    {
        slots[0].item = slots[1].item;
        slots[1].item = null;

        UpdateConsumUI();
    }
}
