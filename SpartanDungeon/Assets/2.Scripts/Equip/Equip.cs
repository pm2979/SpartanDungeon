using UnityEngine;

public abstract class Equip : MonoBehaviour
{
    public Player player;

    public abstract void PassiveOn();


    public abstract void PassiveOff();
}
