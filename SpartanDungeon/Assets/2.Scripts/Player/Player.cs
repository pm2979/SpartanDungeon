using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerController controller;
    PlayerCondition condition;

    private void Awake()
    {
        PlayerManager.Instance.Player = this;

        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}
