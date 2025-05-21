using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public Transform slotPanel;
    public Transform dropPosition;

    private PlayerController controller;
    private PlayerCondition condition;

    void Start()
    {
        controller = PlayerManager.Instance.Player.controller;
        condition = PlayerManager.Instance.Player.condition;

        controller.itemUse += ItemActive;
        PlayerManager.Instance.Player.addItem += AddItem;

        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
        }
    }

    void AddItem() // �������� ����� ��
    {
        ItemData data = PlayerManager.Instance.Player.itemData;

        // ����ִ� ������ �����´�.
        ItemSlot emptySlot = GetEmptySlot();

        // �ִٸ�
        if (emptySlot != null)
        {
            emptySlot.item = data;
            UpdateUI();
            PlayerManager.Instance.Player.itemData = null;
            return;
        }

        // ���ٸ�
        ThrowItem(data);

        PlayerManager.Instance.Player.itemData = null;
    }

    void UpdateUI()
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

    ItemSlot GetEmptySlot() // ����ִ� ������ �����´�.
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

    void ThrowItem(ItemData data) // ������ ������
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    private void ItemActive() // ������ ���
    {
        if (slots[0].item != null)
        {
            for (int i = 0; i < slots[0].item.consumables.Length; i++)
            {
                switch (slots[0].item.consumables[i].type)
                {
                    case CONSUMABLETYPE.HEALTH:
                        condition.Heal(slots[0].item.consumables[i].value);
                        break;
                }
            }

            RemoveSelectedItem();
        }
    }

    void RemoveSelectedItem() // ������ ���� �ڿ� ������ ������
    {
        slots[0].item = slots[1].item;
        slots[1].item = null;

        UpdateUI();
    }
}
