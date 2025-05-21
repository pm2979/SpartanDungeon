using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

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
        ItemSlot emptySlot = GetEmptySlot();

        // 있다면
        if (emptySlot != null)
        {
            emptySlot.item = _data;
            UpdateUI();

            return;
        }

        // 없다면
        ThrowItem(_data);
    }

    private void UpdateUI()
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

            RemoveSelectedItem();
        }
    }

    private void RemoveSelectedItem() // 아이템 사용시 뒤에 아이템 앞으로
    {
        slots[0].item = slots[1].item;
        slots[1].item = null;

        UpdateUI();
    }
}
