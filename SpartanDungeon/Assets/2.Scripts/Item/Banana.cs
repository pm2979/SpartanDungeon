using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : ItemObject
{
    public override void ItemActivate()
    {
        EventBus.Publish("ChangeSpeed", data.consumables[0].value);
        Invoke("BananaDeactivate", data.consumables[0].time);
    }

    private void BananaDeactivate()
    {

        EventBus.Publish("ChangeSpeed", -data.consumables[0].value);
    } 
}

