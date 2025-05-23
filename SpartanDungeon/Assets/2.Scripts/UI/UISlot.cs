using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    public Image icon;

    public void Set(ItemData itemData)
    {
        if(itemData != null)
        {
            icon.sprite = itemData.icon;
        }
        else
        {
            Clear();
        }
    }

    public void Clear()
    {
        icon.sprite = null;
    }
}
