﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTile : MonoBehaviour
{
    private LevelCreator creator;
    private PlayerStatus player;

    private void Awake()
    {
        creator = GetComponentInParent<LevelCreator>();
        player = GameObject.Find("Player").GetComponent<PlayerStatus>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && player.haveKey == true )
        {
            creator.DropUpperFloor();
            Destroy(this);
        }
    }

}
