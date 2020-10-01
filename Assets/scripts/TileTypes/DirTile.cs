/*
    Created by Rob Swor for TRP-2020.
    
    Defines a tile as a direction changer.
    Changes the direction the player is facing.

    Contains a static instance of VoidDirParam 
        for when direction is changed, for all Dir
        and Control switch tiles (and the player) to
        sub to and rotate when called.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirTile : Tile
{
    public static VoidDirParam SpinTime;

    [SerializeField]
    private BlockDir dir = BlockDir.NORTH;

    public override void TileAction()
    {
        // TODO
        if (TileRotation.curDir == dir) return;
        
    }

}
