using UnityEngine;

public class Equipment : MonoBehaviour
{
    private Equip curEquip;

    private void OnEnable()
    {
        EventBus.Subscribe("AddItem", NewEquip);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe("AddItem", NewEquip);
    }

    private void NewEquip(object data)
    {
        ItemData item = (ItemData) data;

        if (item.type != ITEMTYPE.EQUIPABLE) return;

        UnEquip();
        curEquip = Instantiate(item.equipPrefab, transform).GetComponent<Equip>();
        curEquip.player = GetComponent<Player>();
        curEquip.PassiveOn();
        
    }

    private void UnEquip()
    {
        if (curEquip != null)
        {
            curEquip.PassiveOff();
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}
