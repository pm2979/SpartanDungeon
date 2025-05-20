using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition stamina;

    void Start()
    {
        // 플레이어에게 컨디션 할당
        PlayerManager.Instance.Player.condition.uiCondition = this;
    }
}
