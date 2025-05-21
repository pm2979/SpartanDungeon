using System;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    Condition Health { get { return uiCondition.health; } }
    Condition Stamina { get { return uiCondition.stamina; } }

    public event Action onTakeDamage;

    private void Update()
    {
        Stamina.Add(Stamina.pausedValue * Time.deltaTime);

        if(Health.curValue <= 0.0f)
        {
            Die();
        }
    }

    private void OnEnable()
    {
        EventBus.Subscribe("Heal", Heal);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe("Heal", Heal);
    }

    public void Heal(object evt) // 회복
    {
        float amout = (float) evt;
        Health.Add(amout);
    }

    public void TakePhysicalDamager(int damage) // 데미지를 받았을 때 로직
    {
        Health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount) // 스테미나 사용
    {
        if (Stamina.curValue - amount < 0f)
        {
            return false;
        }

        Stamina.Subtract(amount);
        return true;
    }

    public void Die()
    {
        Debug.Log("죽었다.");
    }
}
