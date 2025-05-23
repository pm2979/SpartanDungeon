using System.Collections;
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

    Coroutine statBuff;

    public void StatBuff(float amount, float duration, CONSUMABLETYPE type) // 버프
    {
        // 이미 버프 중이면 먼저 해제
        if (statBuff != null)
            StopCoroutine(statBuff);

        statBuff = StartCoroutine(StatBuffCoroutine(amount, duration, type));
    }

    private IEnumerator StatBuffCoroutine(float amount, float duration, CONSUMABLETYPE type) // 버프 코루틴
    {
        if(type == CONSUMABLETYPE.SPEED)
        {
            moveSpeed += amount;
            yield return new WaitForSeconds(duration);
            moveSpeed -= amount;
        }
        else if(type == CONSUMABLETYPE.JUMP)
        {
            jumpPower += amount;
            
            jumpPower -= amount;
        }
    }
}
