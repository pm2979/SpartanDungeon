using UnityEngine;

public interface IInteractable // ��ȣ�ۿ� �������̽�
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt() // ������ ����
    {
        string str = $"{data.displayName}\n{data.description}";

        return str;
    }

    public void OnInteract() // ��ȣ�ۿ� �� �÷��̾�� ���� �ѱ�
    {
        PlayerManager.Instance.Player.itemData = data;
        PlayerManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}
