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

    private void AddItem(object data) // �������� ����� ��
    {
        var _data = (ItemData) data;
        // ����ִ� ������ �����´�.

        if(_data.type == ITEMTYPE.EQUIPABLE)
        {
            AddEquipable(_data);
        }
        else
        {
            AddConsumable(_data);
        }
    }

    private void AddEquipable(ItemData data) // ��� ������ ȹ�� ��
    {
        if (equipSlot.item != null) // ���� ��� ������
        {
            ThrowItem(equipSlot.item);
        }

        equipSlot.item = data;
        UpdateEquipUI();
    }

    private void AddConsumable(ItemData data) // �Һ� ������ ȹ�� ��
    {
        ItemSlot emptySlot = GetEmptySlot();

        // �ִٸ�
        if (emptySlot != null)
        {
            emptySlot.item = data;
            UpdateConsumUI();

            return;
        }

        ThrowItem(data);
    }

    private void UpdateConsumUI() // �Һ� ������ UI
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

    private void UpdateEquipUI() // ��� ������ UI
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

    private ItemSlot GetEmptySlot() // ����ִ� ������ �����´�.
    {
        for (int i = 0; i < slots.Length; i++)
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
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    private void ItemActive(object evt) // ������ ���
    {
        if (slots[0].item != null)
        {
            slots[0].item.active?.Invoke();

            RemoveConsumableItem();
        }
    }

    private void RemoveConsumableItem() // ������ ���� �ڿ� ������ ������
    {
        slots[0].item = slots[1].item;
        slots[1].item = null;

        UpdateConsumUI();
    }
}
