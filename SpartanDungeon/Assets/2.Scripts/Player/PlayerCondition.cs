using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCondition : MonoBehaviour
{
    //public UICondition uiCondition;

    Condition Health;
    Condition Stamina;
    public event Action onTakeDamage;
    private void Start()
    {

    }

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

    public void Heal(object evt) // ȸ��
    {
        float amout = (float) evt;
        Health.Add(amout);
    }

    public void TakePhysicalDamager(int damage) // �������� �޾��� �� ����
    {
        Health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount) // ���׹̳� ���
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
        Debug.Log("�׾���.");
    }
}
