public class Ax : Equip
{
    public int upSpeed = 5;

    public override void PassiveOn()
    {
        player.StatHandler.MoveSpeed += upSpeed;
    }

    public override void PassiveOff()
    {
        player.StatHandler.MoveSpeed -= upSpeed;
    }
}
