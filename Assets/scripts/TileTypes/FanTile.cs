/*
    Created by Rob Swor for TRP-2020.
    
    Defines a tile as a Fan tile.
    When a player lands here, launches them
        two tiles away if powered on.
    If powered off, nothing happens.

    Can be toggled by a FanSwitchTile.

    Contains a static List<> of all FanTiles on
        the level, for the switch to toggle.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTile : Tile
{
    public static List<FanTile> fanTiles;

    [SerializeField]
    private bool fanOn = false;

    [SerializeField]
    private BlockDir dir = BlockDir.NORTH;
    private Tile destTile;

    void Awake()
    {
        type = TileType.FAN;

        if (fanTiles == null) fanTiles = new List<FanTile>();
        fanTiles.Add(this);

    }

    protected override void TileStart()
    {
        getDestination();
    }

    void OnDisable()
    {
        fanTiles.Remove(this);
    }

    public override void TileAction()
    {
        if (!fanOn) return;

        // TODO
    }

    public void Toggle()
    {
        fanOn = !fanOn;
    }

    private void getDestination()
    {
        // Get an adjacent tile.
        switch(dir) 
        {
            case BlockDir.WEST:
                if (this.west != null)
                {
                    destTile = this.west.west;
                }
                break;
            case BlockDir.EAST:
                if (this.east != null)
                {
                    destTile = this.east.east;
                }
                break;
            case BlockDir.SOUTH:
                if (this.south != null)
                {
                    destTile = this.south.south;
                }
                break;
            case BlockDir.NORTH:
            default:
                if (this.north != null)
                {
                    destTile = this.north.north;
                }
                break;
        }
        
        //If there isn't a block adjacent, check 2 away
        //Fans can launch over gaps, too.
        if (destTile == null)
        {
            Vector3 castDir;
            switch(dir)
            {
                case BlockDir.WEST:
                    castDir = Vector3.left;
                    break;
                case BlockDir.EAST:
                    castDir = Vector3.right;
                    break;
                case BlockDir.SOUTH:
                    castDir = Vector3.back;
                    break;
                case BlockDir.NORTH:
                default:
                    castDir = Vector3.forward;
                    break;
            }
            destTile = RaycastToTile(castDir, 2.0f);
            if (destTile == null)
            {
                Debug.LogError("Not destination for '" + name + "' found.");
            }
            return;
        }
    }
}
