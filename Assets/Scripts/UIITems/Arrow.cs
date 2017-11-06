using UnityEngine;

public class Arrow : MonoBehaviour {

    private LevelCreator level;
    private PlayerStatus player;
    private PlayerControlls playerControlls;
    private float destination;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStatus>();
        playerControlls = GameObject.Find("Player").GetComponent<PlayerControlls>();
        level = GameObject.FindWithTag("LevelCreator").GetComponent<LevelCreator>();
    }

	void Update () {
        // check destination - key or exit
        if (player.haveKey)
        {
            destination = (level.actualFloor.exitTileNumber * 2);
        }
        else
        {
            destination = (level.actualFloor.keyTileNumber * 2);
        }
        // rotate
        if(!player.haveKey || 
            !(player.transform.position.x == (level.actualFloor.exitTileNumber * 2) && player.haveKey))
        {
            if (destination < playerControlls.transform.position.x)
            {
                if (this.transform.rotation.y == 1)
                {
                    this.transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));
                }
            }
            else
       if (destination > playerControlls.transform.position.x)
            {
                if (this.transform.rotation.y == 0)
                {
                    this.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
                }
            }
        }
	}
}
