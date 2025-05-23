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

    public void PausedStamian() // ���׹̳� ���� ȸ��
    {
        Add(pausedValue * Time.deltaTime);

        UpdateStaminaUI();
    }

    public bool UseStamina(float amount) // ���׹̳� ���
    {
        if (curValue - amount < 0f)
        {
            return false;
        }

        Subtract(amount);

        UpdateStaminaUI();
        return true;
    }

    private void UpdateStaminaUI()
    {
        EventBus.Publish("StaminaUpdate", curValue / maxValue);
    }
}
