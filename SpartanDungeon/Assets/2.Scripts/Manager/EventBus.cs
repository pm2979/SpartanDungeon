using System;
using System.Collections.Generic;

public static class EventBus
{
    // �̺�Ʈ Ű�� ������ ����
    private static Dictionary<string, Action<object>> _subscribers = new();

    // ���� ���
    public static void Subscribe(string eventKey, Action<object> callback)
    {
        if (string.IsNullOrEmpty(eventKey) || callback == null) return; // null üũ

        // TryGetValue�� Ű�� ����� ��������Ʈ Ȯ��
        if (_subscribers.TryGetValue(eventKey, out var exist))
        {
            _subscribers[eventKey] = exist + callback;
        }
        else
        {
            _subscribers[eventKey] = callback;
        }
    }

    // ���� ����
    public static void Unsubscribe(string eventKey, Action<object> callback)
    {
        if (string.IsNullOrEmpty(eventKey) || callback == null) return; // null üũ
        if (!_subscribers.TryGetValue(eventKey, out var exist)) return; // ���� Ȯ��

        var updated = exist - callback;
        if (updated == null) // ��������Ʈ�� ��� ���ŵ� ���¸� Ű ����
            _subscribers.Remove(eventKey);
        else // ���� ��������Ʈ ����
            _subscribers[eventKey] = updated;
    }

    // �̺�Ʈ ����
    public static void Publish(string eventKey, object data = null)
    {
        if (string.IsNullOrEmpty(eventKey)) return; // null üũ
        if (!_subscribers.TryGetValue(eventKey, out var callbacks)) return; // ���� Ȯ��

        callbacks?.Invoke(data); // ��������Ʈ ȣ��
    }
}