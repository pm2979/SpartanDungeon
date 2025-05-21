using UnityEngine;
using UnityEngine.UI;

public abstract class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float pausedValue;

    protected virtual void Start()
    {
        // ���� ��
        curValue = startValue;
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
