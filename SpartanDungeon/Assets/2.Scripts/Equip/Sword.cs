public class Sword : Equip
{

    public override void PassiveOn()
    {
        player.Controller.maxJumpIndex++;
    }

    public override void PassiveOff()
    {
        player.Controller.maxJumpIndex--;
    }
}

