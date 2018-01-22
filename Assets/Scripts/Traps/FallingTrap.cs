using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour
{

    private bool isActive;
    private float speed = 9.0f;


    // Use this for initialization
    void Start()
    {
        isActive = false;
        int lvlPassed = GameObject.FindWithTag("GameStatsCounter").GetComponent<GameStatsCounter>().levelsPassedCount;
        transform.position = new Vector3(transform.position.x, transform.position.y + 10.0f - 3.7f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            float playerX = GameObject.FindWithTag("Player").GetComponent<PlayerControlls>().transform.position.x;
            float playerY = GameObject.FindWithTag("Player").GetComponent<PlayerControlls>().transform.position.y;
            if (this.transform.position.x + 1.3f >= playerX &&
                this.transform.position.x - 1.3f <= playerX &&
                this.transform.position.y - playerY <= 10.0f)
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

        if (transform.position.y < 1.0f)
        {
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}