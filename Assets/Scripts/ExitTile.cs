using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTile : MonoBehaviour
{
    private LevelCreator creator;
    private PlayerStatus player;
    private PlayerControlls playerControlls;

    private void Awake()
    {
        creator = GetComponentInParent<LevelCreator>();
        player = GameObject.Find("Player").GetComponent<PlayerStatus>();
        playerControlls = GameObject.Find("Player").GetComponent<PlayerControlls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && player.haveKey == true )
        {
            creator.playerTriggerDrop = true;
            creator.DropUpperFloor();
            playerControlls.fireFloor = false;
            playerControlls.bouncyFloor = false;
            Destroy(this);
        }
    }

}
