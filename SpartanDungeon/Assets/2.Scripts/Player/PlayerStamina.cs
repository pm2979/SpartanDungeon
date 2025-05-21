using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : Condition
{
    protected override void Update()
    {
        base.Update();
        PausedStamian();
    }

    public void PausedStamian() // ���׹̳� ���� ȸ��
    {
        Add(pausedValue * Time.deltaTime);

        EventBus.Publish("StaminaUpdate", curValue / maxValue);
    }

    public bool UseStamina(float amount) // ���׹̳� ���
    {
        if (curValue - amount < 0f)
        {
            return false;
        }

        Subtract(amount);

        EventBus.Publish("StaminaUpdate", curValue / maxValue);
        return true;
    }
}
