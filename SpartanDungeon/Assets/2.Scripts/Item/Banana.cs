
public class Banana : ItemObject, IConsumable
{
    public void ItemActivate(Player player)
    {
        player.StatHandler.StatBuff(
            data.consumables[0].value,
            data.consumables[0].time,
            data.consumables[0].type
            );
    }
}

