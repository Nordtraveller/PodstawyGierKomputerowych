using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTile : MonoBehaviour
{
    private PlayerControlls player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControlls>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            if(transform.position.x / GameMetrics.tileHorizontalSize == GetComponentInParent<Floor>().GetExitTileNumber() 
                && GameObject.FindWithTag("Player").GetComponent<PlayerStatus>().haveKey == true)
            {
                player.fireFloor = false;
            }
            else
            {
                player.fireFloor = true;
            }
        }
    }

}
