using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;

    public ItemData itemData;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }
}
