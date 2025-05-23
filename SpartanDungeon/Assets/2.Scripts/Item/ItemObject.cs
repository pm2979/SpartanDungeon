using UnityEngine;

public interface IInteractable // ��ȣ�ۿ� �������̽�
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public interface IConsumable // �Ҹ�ǰ �������̽�
{
    public void ItemActivate(Player player);
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt() // ������ ����
    {
        string str = $"{data.displayName}\n{data.description}";

        return str;
    }

    public void OnInteract()
    {
        Destroy(gameObject);
    }
}
