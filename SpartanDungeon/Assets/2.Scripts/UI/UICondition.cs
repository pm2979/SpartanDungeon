using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition stamina;

    void Start()
    {
        // �÷��̾�� ����� �Ҵ�
        PlayerManager.Instance.Player.condition.uiCondition = this;
    }
}
