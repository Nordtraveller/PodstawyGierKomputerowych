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
            creator.DropUpperFloor();
            playerControlls.onFire = false;
            Destroy(this);
        }
    }

}
