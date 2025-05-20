using UnityEngine;

public interface IInteractable // 상호작용 인터페이스
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt() // 아이템 정보
    {
        string str = $"{data.displayName}\n{data.description}";

        return str;
    }

    public void OnInteract() // 상호작용 시 플레이어에게 정보 넘김
    {
        PlayerManager.Instance.Player.itemData = data;
        PlayerManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}
