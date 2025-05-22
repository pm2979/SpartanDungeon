using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Range(1, 100)][SerializeField] private float health = 10;
    public float Health
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0, 100); }
    }

    [Range(1, 200)][SerializeField] private float stamina = 200;
    public float Stamina
    {
        get { return stamina; }
        set { stamina = Mathf.Clamp(value, 0, 200); }
    }

    [Range(1f, 30f)][SerializeField] private float moveSpeed = 5;
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = Mathf.Clamp(value, 0, 30);
    }

    [Range(1f, 150f)][SerializeField] private float jumpPower = 3;
    public float JumpPower
    {
        get => jumpPower;
        set => jumpPower = Mathf.Clamp(value, 0, 150);
    }

    private void OnEnable()
    {
        EventBus.Subscribe("ChangeSpeed", ChangeSpeed);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe("ChangeSpeed", ChangeSpeed);
    }

    private void ChangeSpeed(object amount)
    {
        MoveSpeed += (float)amount;
    }

}
