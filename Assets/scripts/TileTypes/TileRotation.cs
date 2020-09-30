using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRotation : MonoBehaviour
{

    //Subscribe to delegates.
    void Start()
    {
        DirTile.SpinTime += RotateBlock;
    }

    void OnDisable()
    {
        DirTile.SpinTime -= RotateBlock;
    }

    private void RotateBlock(Tile.BlockDir newDir)
    {
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
    }
}
