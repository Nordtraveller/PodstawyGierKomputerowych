using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    private bool isActive;
    private float speed = 9.0f;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        isActive = false;
        transform.position = new Vector3(transform.position.x, transform.position.y + GameMetrics.upperFloorY - 4.0f, transform.position.z);
    }

    void Update()
    {
        if (!isActive)
        {
            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;
            if (this.transform.position.x + GameMetrics.fallingTrapXOffset >= playerX &&
                this.transform.position.x - GameMetrics.fallingTrapXOffset <= playerX &&
                this.transform.position.y - playerY <= GameMetrics.upperFloorY)
            {
                this.isActive = true;
            }
        }

        if (isActive)
        {
            float step = speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y - step, transform.position.z);
        }

        if (transform.position.y < 1.0f)
        {
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}