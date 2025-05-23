public class Apple : ItemObject, IConsumable
{
    public void ItemActivate(Player player)
    {
        for (int i = 0; i < data.consumables.Length; i++)
        {
            switch (data.consumables[i].type)
            {
                case CONSUMABLETYPE.HEALTH:
                    player.PlayerHP.Heal(data.consumables[i].value);
                    break;
                case CONSUMABLETYPE.STAMINA:
                    player.PlayerStamina.Heal(data.consumables[i].value);
                    break;
            }
        }
    }
}
