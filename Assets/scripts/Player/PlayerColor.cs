/*
    Created by Rob Swor for TRP-2020.
    
    This goes on the Hat and Cape objects and watches
        for when the rightmode is flipped.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    private Material material;

    void Start() {
        material = GetComponent<Renderer>().sharedMaterial;
        //Subscribe
        Player.RightModeUpdate += ColorChange;
    }
    
    void OnDestroy() {
        //Unsubscribe
        Player.RightModeUpdate -= ColorChange;
    }

    private void ColorChange(bool onlyRight) {
        if (onlyRight) {
            material.color = Color.red;
        } else {
            material.color = Color.blue;
        }
    }
}
