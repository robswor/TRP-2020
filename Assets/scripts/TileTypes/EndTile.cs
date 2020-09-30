/*
    Created by Rob Swor for TRP-2020.
    
    Defines a tile as a goal tile.

    Has a static member 'goal' that should
        be a reference to the goal of each level.
        Done to require fewer tiles referencing each other.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTile : Tile
{
    // Static instance per level
    public static EndTile goal;

    [SerializeField]
    private bool keyCollected = false;

    void Awake()
    {
        type = TileType.END;
        goal = this;
    }

    public override void TileAction()
    {
        if (!keyCollected) return;

        // TODO: End level
    }

    public void GiveKey()
    {
        keyCollected = true;
    }

}
