using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEntranceTile : MonoBehaviour
{
    private PlayerStatus player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStatus>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(player.haveKey)
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
