using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public int equipSlotNum = 0;
    public int slotCount = 3;
    public Transform dropPosition;
    
    private InputHandler inputHandler;
    private Equipment equipment;

    public void Init(InputHandler inputHandler, Equipment equipment)
    {
        this.inputHandler = inputHandler;
        this.equipment = equipment;

        slots = new ItemSlot[slotCount];
        for (int i = 0; i < slotCount; i++)
            slots[i] = new ItemSlot(null);
    }

    private void Update()
    {
        if(inputHandler.isUseItme == true)
        {
            ItemActivate();
            RemoveConsumableItem();
            inputHandler.isUseItme = false;
        }
    }

    public void AddItem(ItemData data) // 아이템을 얻었을 때
    {
        if (data.type == ITEMTYPE.EQUIPABLE)
        {
            AddEquipable(data);
        }
        else
        {
            AddConsumable(data);
        }
    }

    private void AddEquipable(ItemData data) // 장비 아이템 획득 시
    {
        if (slots[equipSlotNum].item != null) // 기존 장비 버리기
        {
            ThrowItem(slots[equipSlotNum].item);
        }

        slots[equipSlotNum].item = data;
        equipment.NewEquip(data);

        UpdateUI();
    }

    private void AddConsumable(ItemData data) // 소비 아이템 획득 시
    {
        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = data;

            UpdateUI();
            return;
        }

        ThrowItem(data);
    }

    private ItemSlot GetEmptySlot() // 비어있는 슬롯을 가져온다.
    {
        for (int i = equipSlotNum + 1; i < slots.Length; i++)
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

    private void ItemActivate() // 아이템 사용
    {
        if (slots[1].item != null)
        {
            slots[1].item.active?.Invoke();

            //RemoveConsumableItem();
        }
    }

    private void RemoveConsumableItem() // 아이템 사용시 뒤에 아이템 앞으로
    {
        slots[1].item = slots[2].item;
        slots[2].item = null;

        UpdateUI();
    }

    private void UpdateUI() // UI 갱신
    {
        EventBus.Publish("UpdateItemUI", slots);
    }
}
