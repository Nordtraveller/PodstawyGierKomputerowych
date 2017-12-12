using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour {

    private bool isActive;
    private float startYPosition;
    private float endYPosition;
    private float speed = 8.5f;
    PlayerControlls player;


    // Use this for initialization
    void Start () {
        do
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControlls>();
        }
        while (player == null);
        isActive = false;

        startYPosition =(float)GameObject.FindWithTag("LevelCreator").GetComponent<LevelCreator>()
            .getUpperFloor().transform.position.y;
        endYPosition = (float)GameObject.FindWithTag("LevelCreator").GetComponent<LevelCreator>()
                                                   .actualFloor.transform.position.y;
        transform.position = new Vector3(transform.position.x, startYPosition -1.3f, transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
	    if (!isActive)
	    {
	        float playerX = player.transform.position.x;
            if (((this.transform.position.x + 1.3f) >= playerX) &&
                ((this.transform.position.x - 1.3f) <= playerX))
            {
                this.isActive = true;
            }
	    }

        if (isActive)
        {
            float step = speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y - step,
            transform.position.z);
        }

        if (transform.position.y < endYPosition +1.0f)
        {
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
