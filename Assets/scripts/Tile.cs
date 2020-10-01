using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {


    public delegate void VoidDirParam(BlockDir param);

    // This is where the player stands on the object.
    [SerializeField]
    private Transform playerPoint;

    public enum BlockDir {
        NORTH = 0,
        SOUTH = 1,
        EAST = 2,
        WEST = 3
    }

    // Use this for initialization
//    public Tile north = null, south = null, east = null, west = null;

    public void Start()
    {
        TileStart();
    }

    // Called when player lands on the space.
    // For normal tiles, do nothing.
    public virtual void TileAction() { return; }

    // Called in Start so that Start doesn't need to be overridden.
    // Does nothing at base.
    protected virtual void TileStart() { return; }

    // Raycasts to try and find adjacent tiles.
    public Tile GetAdjacentTile(Vector3 dir, float dist = 1.0f)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir.normalized, out hit, dist))
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

    public Transform GetPlayerPoint()
    {
        return playerPoint != null ? playerPoint : transform;
    }
}
