
public class Banana : ItemObject, IConsumable
{
    public void ItemActivate()
    {
        EventBus.Publish("ChangeSpeed", data.consumables[0].value);
        Invoke("BananaDeactivate", data.consumables[0].time);
    }

    private void BananaDeactivate()
    {

        EventBus.Publish("ChangeSpeed", -data.consumables[0].value);
    } 
}

