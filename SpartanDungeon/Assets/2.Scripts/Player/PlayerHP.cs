using UnityEngine;

public class PlayerHP : Condition
{
    private StatHandler statHandler;

    private void Awake()
    {
        statHandler = GetComponent<StatHandler>();
        maxValue = statHandler.Health;
    }

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

    public void Heal(object amount) // ȸ��
    {
        Add((float)amount);
        EventBus.Publish("HpUpdate", curValue / maxValue);
    }

    public void TakePhysicalDamager(int damage) // �������� �޾��� �� ����
    {
        Subtract(damage);
        EventBus.Publish("HpUpdate", curValue / maxValue);
    }

    public void Die()
    {
        Debug.Log("�׾���.");
    }
}
