using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTile : Tile
{
    public bool keyCollected = false;
    public GameObject keyObj;

    void Awake()
    {
        type = TileType.KEY;
    }

    public override void TileAction()
    {
        if (keyCollected) return;

        keyCollected = true;
        keyObj.SetActive(false);

        // Unlock the goal
        EndTile.goal.GiveKey();
    }
}
