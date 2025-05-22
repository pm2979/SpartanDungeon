public class Apple : ItemObject, IConsumable
{
    public void ItemActivate()
    {
        for (int i = 0; i < data.consumables.Length; i++)
        {
            EventBus.Publish("Heal", data.consumables[i].value);
        }
    }
}
