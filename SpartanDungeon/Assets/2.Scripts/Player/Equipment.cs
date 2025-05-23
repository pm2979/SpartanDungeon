using UnityEngine;

public class Equipment : MonoBehaviour
{
    private Equip curEquip;

    public void NewEquip(ItemData item) // 새로운 장비 장착
    {
        UnEquip();

        curEquip = Instantiate(item.equipPrefab, transform).GetComponent<Equip>();
        curEquip.player = GetComponent<Player>();
        curEquip.PassiveOn();
    }

    private void UnEquip() // 장비 해제
    {
        if (curEquip != null)
        {
            curEquip.PassiveOff();
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}
