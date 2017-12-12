using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour {

    private bool isActive;
    private float startYPosition;
    private float endYPosition;
    private Vector3 start;
    private Vector3 target;
    private float speed = 14f;


    // Use this for initialization
    void Start () {
        isActive = false;

        startYPosition =(float)GameObject.FindWithTag("LevelCreator").GetComponent<LevelCreator>()
            .getUpperFloor().transform.position.y;
        endYPosition = (float)GameObject.FindWithTag("LevelCreator").GetComponent<LevelCreator>()
                                                   .actualFloor.transform.position.y;
        transform.position = new Vector3(transform.position.x, startYPosition -1.3f, transform.position.z);



    }
	
	// Update is called once per frame
	void Update () {
        float playerX = GameObject.FindWithTag("Player").GetComponent<PlayerControlls>().transform.position.x;
        if (((this.transform.position.x + 1.3f) >= playerX) &&
            ((this.transform.position.x - 1.3f) <= playerX))
        {
            this.isActive = true;
        }

        if (isActive)
        {
            float step = speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y - step,
            transform.position.z);
        }

        if (transform.position.y == endYPosition+2f)
        {
            Destroy(gameObject);
        }
    }
}
