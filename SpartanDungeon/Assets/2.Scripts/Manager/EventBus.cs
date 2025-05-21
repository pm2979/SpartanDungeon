using System;
using System.Collections.Generic;

public static class EventBus
{
    // 이벤트 키별 구독자(Action<object>) 보관
    private static readonly Dictionary<string, Action<object>> _subscribers = new();

    // 구독 등록
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

    // 구독 해제
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

    // 이벤트 발행
    public static void Publish(string eventKey, object data = null)
    {
        if (string.IsNullOrEmpty(eventKey)) return;
        if (!_subscribers.TryGetValue(eventKey, out var callbacks)) return;

        callbacks?.Invoke(data);
    }
}