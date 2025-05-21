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
        // 시작 값
        curValue = startValue;
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
