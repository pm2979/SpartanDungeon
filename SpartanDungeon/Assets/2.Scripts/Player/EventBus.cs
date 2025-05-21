using System;
using System.Collections.Generic;

public static class EventBus
{
    // �̺�Ʈ Ű�� ������(Action<object>) ����
    private static readonly Dictionary<string, Action<object>> _subscribers = new();

    // ���� ���
    public static void Subscribe(string eventKey, Action<object> callback)
    {
        if (string.IsNullOrEmpty(eventKey) || callback == null) return;

        if (_subscribers.TryGetValue(eventKey, out var existing))
        {
            _subscribers[eventKey] = existing + callback;
        }
        else
        {
            _subscribers[eventKey] = callback;
        }
    }

    // ���� ����
    public static void Unsubscribe(string eventKey, Action<object> callback)
    {
        if (string.IsNullOrEmpty(eventKey) || callback == null) return;
        if (!_subscribers.TryGetValue(eventKey, out var existing)) return;

        var updated = existing - callback;
        if (updated == null)
            _subscribers.Remove(eventKey);
        else
            _subscribers[eventKey] = updated;
    }

    // �̺�Ʈ ����
    public static void Publish(string eventKey, object data = null)
    {
        if (string.IsNullOrEmpty(eventKey)) return;
        if (!_subscribers.TryGetValue(eventKey, out var callbacks)) return;

        callbacks?.Invoke(data);
    }
}