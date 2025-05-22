public class Ax : Equip
{
    public override void PassiveOn()
    {
        player.StatHandler.MoveSpeed += 5;
    }

    public override void PassiveOff()
    {
        player.StatHandler.MoveSpeed -= 5;
    }
}
