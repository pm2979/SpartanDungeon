using System;
using System.Collections.Generic;

public static class EventBus
{
    // 이벤트 키별 구독자 보관
    private static Dictionary<string, Action<object>> _subscribers = new();

    // 구독 등록
    public static void Subscribe(string eventKey, Action<object> callback)
    {
        if (string.IsNullOrEmpty(eventKey) || callback == null) return; // null 체크

        // TryGetValue로 키에 연결된 델리게이트 확인
        if (_subscribers.TryGetValue(eventKey, out var exist))
        {
            _subscribers[eventKey] = exist + callback;
        }
        else
        {
            _subscribers[eventKey] = callback;
        }
    }

    // 구독 해제
    public static void Unsubscribe(string eventKey, Action<object> callback)
    {
        if (string.IsNullOrEmpty(eventKey) || callback == null) return; // null 체크
        if (!_subscribers.TryGetValue(eventKey, out var exist)) return; // 구독 확인

        var updated = exist - callback;
        if (updated == null) // 델리게이트가 모두 제거된 상태면 키 삭제
            _subscribers.Remove(eventKey);
        else // 남은 델리게이트 저장
            _subscribers[eventKey] = updated;
    }

    // 이벤트 발행
    public static void Publish(string eventKey, object data = null)
    {
        if (string.IsNullOrEmpty(eventKey)) return; // null 체크
        if (!_subscribers.TryGetValue(eventKey, out var callbacks)) return; // 구독 확인

        callbacks?.Invoke(data); // 델리게이트 호출
    }
}