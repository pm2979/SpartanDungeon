using System;
using UnityEngine;

public class PlayerHP : Condition
{
    protected override void Start()
    {
        base.Start();
        EventBus.Publish("HpUpdate", curValue / maxValue);
    }

    protected override void Update()
    {
        base.Update();
        if (curValue <= 0.0f)
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
        Add(amout);
        EventBus.Publish("HpUpdate", curValue / maxValue);
    }

    public void TakePhysicalDamager(int damage) // �������� �޾��� �� ����
    {
        Subtract(damage);
    }

    public void Die()
    {
        Debug.Log("�׾���.");
    }
}
