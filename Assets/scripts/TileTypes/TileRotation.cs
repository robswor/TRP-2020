/*
    Created by Rob Swor for TRP-2020.
    
    Tiles with this component will rotate
        when the Player does.
    This should mostly only be on Control
        tiles.

    Contains a static BlockDir that tracks
        the current direction that all blocks
        ought to be facing.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRotation : MonoBehaviour
{
    
    public static Tile.BlockDir curDir = Tile.BlockDir.NORTH;

    //Subscribe to delegates.
    void Start()
    {
        //Sanity check
        curDir = Tile.BlockDir.NORTH;

        DirTile.SpinTime += RotateBlock;
    }

    void OnDisable()
    {
        DirTile.SpinTime -= RotateBlock;
    }

    private void RotateBlock(Tile.BlockDir newDir)
    {
        if (newDir == curDir) return;

        switch (newDir)
        {
            case Tile.BlockDir.NORTH:
            default:
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                break;
            case Tile.BlockDir.SOUTH:
                transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                break;
            case Tile.BlockDir.EAST:
                transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                break;
            case Tile.BlockDir.WEST:
                transform.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
                break;
        }
        curDir = newDir;
    }
}
