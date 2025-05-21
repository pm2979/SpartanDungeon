using UnityEngine;

public interface IInteractable // ��ȣ�ۿ� �������̽�
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public abstract class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt() // ������ ����
    {
        string str = $"{data.displayName}\n{data.description}";

        return str;
    }

    public void OnInteract() // ��ȣ�ۿ� �� �κ��丮�� ���� �ѱ�
    {
        EventBus.Publish("AddItem", data);
        Destroy(gameObject);
    }

    public virtual void ItemActive()
    {

    }
}
