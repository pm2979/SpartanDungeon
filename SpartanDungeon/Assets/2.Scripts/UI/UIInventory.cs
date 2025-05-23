using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private UISlot[] SlotUIs;

    private void OnEnable()
    {
        EventBus.Subscribe("UpdateItemUI", UpdateItemUI);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe("UpdateItemUI", UpdateItemUI);
    }

    private void UpdateItemUI(object data) // ItemSlot UI 업데이트
    {
        ItemSlot[] slots = (ItemSlot[]) data;
        
        for(int i = 0; i < SlotUIs.Length; i++)
        {
            SlotUIs[i].Set(slots[i].item);
        }
    }
}
