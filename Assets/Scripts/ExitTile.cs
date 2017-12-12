using System.Collections;
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
        player = GameObject.Find("Player").GetComponent<PlayerStatus>();
        playerControlls = GameObject.Find("Player").GetComponent<PlayerControlls>();
        gameStatsCounter = GameObject.FindGameObjectWithTag("GameStatsCounter").GetComponent<GameStatsCounter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && player.haveKey == true )
        {
            gameStatsCounter.levelsPassedCount += 1;
            creator.playerTriggerDrop = true;
            creator.DropUpperFloor();
            playerControlls.fireFloor = false;
            playerControlls.windyFloor = false;
            playerControlls.bouncyFloor = false;
            playerControlls.darkFloor = false;
            this.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
            Destroy(this);
        }
    }

}
