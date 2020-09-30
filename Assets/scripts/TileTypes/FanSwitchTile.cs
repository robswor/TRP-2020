/*
    Created by Rob Swor for TRP-2020.
    
    Defines a tile as a fan switch.
    Toggles all fans on the level.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSwitchTile : Tile
{

    // The fan triggered by this switch.
    [SerializeField]
    private FanTile fan = null;

    void Awake()
    {
        type = TileType.FANSWITCH;
    }

    public override void TileAction()
    {
        foreach (FanTile fan in FanTile.fanTiles)
        {
            fan.Toggle();
        }
    }
}
