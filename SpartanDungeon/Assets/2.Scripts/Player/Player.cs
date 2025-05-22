using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController Controller { get; private set; }
    public StatHandler StatHandler { get; private set; }
    public InputHandler InputHandler { get; private set; }
    public Equipment Equipment { get; private set; }
    public PlayerHP PlayerHP { get; private set; }
    public PlayerStamina PlayerStamina { get; private set; }

    private void Awake()
    {
        Controller = GetComponent<PlayerController>();
        StatHandler = GetComponent<StatHandler>();
        InputHandler = GetComponent<InputHandler>();
        Equipment = GetComponent<Equipment>();
        PlayerHP = GetComponent<PlayerHP>();
        PlayerStamina = GetComponent<PlayerStamina>();

        Controller.Init(PlayerStamina, StatHandler, InputHandler);
        PlayerHP.Init(StatHandler);
        PlayerStamina.Init(StatHandler);
    }
}
