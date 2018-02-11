using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceTile : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerStatus player = other.gameObject.GetComponent<PlayerStatus>();
            player.actualFloorType = this.gameObject.GetComponentInParent<Floor>().floorType;
            if (player.haveKey)
            {
                player.haveKey = false;
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(player.gameObject);
            }
        }
    }
}
