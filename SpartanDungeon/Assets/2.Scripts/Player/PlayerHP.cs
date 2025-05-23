using UnityEngine;

public class PlayerHP : Condition
{
    public void Init(StatHandler statHandler)
    {
        maxValue = statHandler.Health;
    }

    protected override void Start()
    {
        base.Start();
        UpdateHpUI();
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

    public void Heal(object amount) // ȸ��
    {
        Add((float)amount);
        UpdateHpUI();
    }

    public void TakePhysicalDamager(int damage) // �������� �޾��� �� ����
    {
        Subtract(damage);
        UpdateHpUI();
    }

    private void UpdateHpUI()
    {
        EventBus.Publish("HpUpdate", curValue / maxValue);
    }

    public void Die()
    {
        Debug.Log("�׾���.");
    }
}
