using UnityEngine;

public class Equipment : MonoBehaviour
{
    private Equip curEquip;

    public void NewEquip(ItemData item) // ���ο� ��� ����
    {
        UnEquip();

        curEquip = Instantiate(item.equipPrefab, transform).GetComponent<Equip>();
        curEquip.player = GetComponent<Player>();
        curEquip.PassiveOn();
    }

    private void UnEquip() // ��� ����
    {
        if (curEquip != null)
        {
            curEquip.PassiveOff();
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}
