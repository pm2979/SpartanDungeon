using UnityEngine;

public interface IInteractable // 상호작용 인터페이스
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public interface IConsumable // 소모품 인터페이스
{
    public void ItemActivate(Player player);
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt() // 아이템 정보
    {
        string str = $"{data.displayName}\n{data.description}";

        return str;
    }

    public void OnInteract()
    {
        Destroy(gameObject);
    }
}
