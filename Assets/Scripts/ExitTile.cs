using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTile : MonoBehaviour
{
    private LevelCreator creator;
    private PlayerStatus player;
    private PlayerControlls playerControlls;
    private GameStatsCounter gameStatsCounter;
    private Vector3 tilePosition;

    private bool floorDrop = false;

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
                tilePosition = this.gameObject.transform.parent.position;
                gameStatsCounter.levelsPassedCount += 1;
                creator.playerTriggerDrop = true;
                floorDrop = true;
                creator.DropUpperFloor();
                Destroy(this.gameObject.GetComponent<Collider2D>());
            }
        }
    }

    private void Update()
    {
        if(floorDrop)
        {
            this.gameObject.transform.parent.position =  Vector3.Lerp(tilePosition,
                new Vector3(tilePosition.x, 0f, tilePosition.z), GameMetrics.dropDuration);
        }
    }
}
