using System;

public class Apple : ItemObject
{
    public override void ItemActivate()
    {
        for (int i = 0; i < data.consumables.Length; i++)
        {
            EventBus.Publish("Heal", data.consumables[i].value);
        }
    }
}
