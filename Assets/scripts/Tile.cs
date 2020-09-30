using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {


    public delegate void VoidDirParam(BlockDir param);

    public enum BlockDir {
        NORTH = 0,
        SOUTH = 1,
        EAST = 2,
        WEST = 3
    }

    public enum TileType {
        NORMIE = 0,
        START = 1,
        END = 2,
        DIR = 3,
        CONTROL = 4,
        KEY = 5,
        FAN = 6,
        FANSWITCH = 7,
        ONETIME = 8 // I don't remember what this is lol
    }

    // Use this for initialization
    public Tile north = null, south = null, east = null, west = null;

    [SerializeField]
    protected TileType type = TileType.NORMIE;

    public void Start()
    {
        GetAdjacency();
        TileStart();
    }

    // Forms a list of adjacent tiles by raycasting in each direction.
    public void GetAdjacency() 
    {
        north = RaycastToTile(Vector3.forward);
        south = RaycastToTile(Vector3.back);
        east = RaycastToTile(Vector3.right);
        west = RaycastToTile(Vector3.left);
    }

    // Called when player lands on the space.
    // For normal tiles, do nothing.
    public virtual void TileAction() { return; }

    // Called in Start so that Start doesn't need to be overridden.
    // Does nothing at base.
    protected virtual void TileStart() { return; }

    public TileType GetTileType()
    {
        return type;
    }

    public void TileDestroy()
    {
        if (north != null)
        {
            north.south = null;
        }

        if (east != null)
        {
            east.west = null;
        }

        if (south != null)
        {
            south.north = null;
        }

        if (west != null)
        {
            west.east = null;
        }

        gameObject.SetActive(false);
    }

    // Raycasts to try and find adjacent tiles.
    protected Tile RaycastToTile(Vector3 dir, float dist = 1.0f)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir.normalized, out hit, 1.0f))
        {
            Tile ret = hit.transform.GetComponent<Tile>();
            if (ret == null)
            {
                Debug.LogError("Object '" + ret.name + "' does not have a Tile component.");
            }
            return ret;
        }
        return null;
    }
}
