using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;

    public Image icon;

    public bool equipped;

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
    }
}
