using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEntranceTile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStatus player = other.gameObject.GetComponent<PlayerStatus>();
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
