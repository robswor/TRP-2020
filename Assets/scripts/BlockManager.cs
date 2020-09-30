using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {

    public Tile[] SwitchList;
    // Use this for initialization
    public void rotateSwitches(Player.Direction newDir)
    {
        for (int i = 0; i < SwitchList.Length; i++)
        {
            switch(newDir){
			case Player.Direction.Up:
                SwitchList[i].transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                break;
            case Player.Direction.Down:
				SwitchList[i].transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
                break;
            case Player.Direction.Left:
				SwitchList[i].transform.rotation = Quaternion.Euler(transform.rotation.x, 270, transform.rotation.z);
                break;
            case Player.Direction.Right:
				SwitchList[i].transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
                break;
            }
        }
    }


}
