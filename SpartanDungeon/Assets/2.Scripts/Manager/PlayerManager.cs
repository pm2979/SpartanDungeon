using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerManager : Singleton<PlayerManager>
{
    Player player;

    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    protected override void Awake()
    {
        base.Awake();

    }


}
