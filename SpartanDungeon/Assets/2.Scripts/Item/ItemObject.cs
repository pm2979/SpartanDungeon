using UnityEngine;

public interface IInteractable // 상호작용 인터페이스
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public abstract class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt() // 아이템 정보
    {
        string str = $"{data.displayName}\n{data.description}";

        return str;
    }

    public void OnInteract() // 상호작용 시 인벤토리에 정보 넘김
    {
        EventBus.Publish("AddItem", data);
        Destroy(gameObject);
    }

    public virtual void ItemActive()
    {

    }
}
