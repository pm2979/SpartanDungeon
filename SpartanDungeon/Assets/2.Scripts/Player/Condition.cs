using UnityEngine;

public abstract class Condition : MonoBehaviour
{
    public float curValue;
    protected float maxValue;
    [SerializeField] protected float pausedValue;

    protected virtual void Start()
    {
        // 시작 값
        curValue = maxValue;
    }

    protected virtual void Update()
    {

    }

    public void Add(float value) // + 값
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Subtract(float value) // - 값
    {
        curValue = Mathf.Max(curValue - value, 0f);
    }
}
