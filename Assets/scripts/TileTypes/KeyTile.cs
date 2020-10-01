using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTile : Tile
{
    private bool keyCollected = false;
    public GameObject keyObj;

    public override void TileAction()
    {
        if (keyCollected) return;

        keyCollected = true;
        keyObj.SetActive(false);

        // Unlock the goal
        EndTile.goal.GiveKey();
    }
}
