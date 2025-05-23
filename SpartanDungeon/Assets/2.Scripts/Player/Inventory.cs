using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public int equipSlotNum = 0;
    public int slotCount = 3;
    public Transform dropPosition;
    
    private Player player;

    public void Init(Player player)
    {
        this.player = player;

        slots = new ItemSlot[slotCount];
        for (int i = 0; i < slotCount; i++)
            slots[i] = new ItemSlot(null);
    }

    private void Update()
    {
        if(player.InputHandler.isUseItme == true)
        {
            ItemActivate();
            player.InputHandler.isUseItme = false;
        }
    }

    public void AddItem(ItemData data) // �������� ����� ��
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

    private void AddEquipable(ItemData data) // ��� ������ ȹ�� ��
    {
        if (slots[equipSlotNum].item != null) // ���� ��� ������
        {
            ThrowItem(slots[equipSlotNum].item);
        }

        slots[equipSlotNum].item = data;
        player.Equipment.NewEquip(data);

        UpdateUI();
    }

    private void AddConsumable(ItemData data) // �Һ� ������ ȹ�� ��
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

    private ItemSlot GetEmptySlot() // ����ִ� ������ �����´�.
    {
        for (int i = equipSlotNum + 1; i < slots.Length; i++)
        {
            if (slots[i].item == null) // ������ ��������� return
            {
                return slots[i];
            }
        }
        return null;
    }

    private void ThrowItem(ItemData data) // ������ ������
    {
        Instantiate(data.prefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    private void ItemActivate() // ������ ���
    {
        if (slots[1].item != null)
        {
            if(slots[1].item.prefab.TryGetComponent<IConsumable>(out IConsumable consumable))
            {
                consumable.ItemActivate(player);
            }

            RemoveConsumableItem();
        }
    }

    private void RemoveConsumableItem() // ������ ���� �ڿ� ������ ������
    {
        slots[1].item = slots[2].item;
        slots[2].item = null;

        UpdateUI();
    }

    private void UpdateUI() // UI ����
    {
        EventBus.Publish("UpdateItemUI", slots);
    }
}
