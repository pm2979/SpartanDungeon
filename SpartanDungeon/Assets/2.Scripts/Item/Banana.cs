using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : ItemObject
{
    public override void ItemActive()
    {
        EventBus.Publish("ChangeSpeed", data.consumables[0].value);
        Invoke("BananaActive", data.consumables[0].time);
    }

    private void BananaActive()
    {

        EventBus.Publish("ChangeSpeed", -data.consumables[0].value);
    } 
}

