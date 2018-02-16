﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTile : MonoBehaviour
{
    private LevelCreator creator;
    private PlayerStatus player;
    private PlayerControlls playerControlls;
    private GameStatsCounter gameStatsCounter;

    private void Awake()
    {
        creator = GetComponentInParent<LevelCreator>();
        gameStatsCounter = GameObject.FindGameObjectWithTag("GameStatsCounter").GetComponent<GameStatsCounter>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerStatus player = other.gameObject.GetComponent<PlayerStatus>();
            PlayerControlls playerControlls = other.gameObject.GetComponent<PlayerControlls>();
            if (player.haveKey == true)
            {
                gameStatsCounter.levelsPassedCount += 1;
                creator.playerTriggerDrop = true;
                creator.DropUpperFloor();
                Destroy(this.gameObject);
            }
        }
    }
}
