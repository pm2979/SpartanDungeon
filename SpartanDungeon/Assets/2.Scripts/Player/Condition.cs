using UnityEngine;

public abstract class Condition : MonoBehaviour
{
    public float curValue;
    protected float maxValue;
    [SerializeField] protected float pausedValue;

    protected virtual void Start()
    {
        // ���� ��
        curValue = maxValue;
    }

    protected virtual void Update()
    {

    }

    public void Add(float value) // + ��
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Subtract(float value) // - ��
    {
        curValue = Mathf.Max(curValue - value, 0f);
    }
}
