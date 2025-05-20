using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float pausedValue;
    public Image uiBar;

    void Start()
    {
        // 시작 값
        curValue = startValue;
    }

    void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage() // UI 표시 값
    {
        return curValue / maxValue;
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
