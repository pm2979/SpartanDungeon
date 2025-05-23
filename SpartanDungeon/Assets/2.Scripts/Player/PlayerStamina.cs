using UnityEngine;

public class PlayerStamina : Condition
{
    public void Init(StatHandler statHandler)
    {
        maxValue = statHandler.Stamina;
    }

    protected override void Update()
    {
        base.Update();
        PausedStamian();
    }

    public void PausedStamian() // 스테미나 지속 회복
    {
        Add(pausedValue * Time.deltaTime);

        UpdateStaminaUI();
    }

    public bool UseStamina(float amount) // 스테미나 사용
    {
        if (curValue - amount < 0f)
        {
            return false;
        }

        Subtract(amount);

        UpdateStaminaUI();
        return true;
    }

    public void Heal(object amount) // 회복
    {
        Add((float)amount);
        UpdateStaminaUI();
    }

    private void UpdateStaminaUI()
    {
        EventBus.Publish("StaminaUpdate", curValue / maxValue);
    }
}
