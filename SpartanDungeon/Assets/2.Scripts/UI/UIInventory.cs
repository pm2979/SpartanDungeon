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

    void AddItem() // 아이템을 얻었을 때
    {
        ItemData data = PlayerManager.Instance.Player.itemData;

        // 비어있는 슬롯을 가져온다.
        ItemSlot emptySlot = GetEmptySlot();

        // 있다면
        if (emptySlot != null)
        {
            emptySlot.item = data;
            UpdateUI();
            PlayerManager.Instance.Player.itemData = null;
            return;
        }

        // 없다면
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

    ItemSlot GetEmptySlot() // 비어있는 슬롯을 가져온다.
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

    void ThrowItem(ItemData data) // 아이템 버리기
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    private void ItemActive() // 아이템 사용
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

    void RemoveSelectedItem() // 아이템 사용시 뒤에 아이템 앞으로
    {
        slots[0].item = slots[1].item;
        slots[1].item = null;

        UpdateUI();
    }
}
