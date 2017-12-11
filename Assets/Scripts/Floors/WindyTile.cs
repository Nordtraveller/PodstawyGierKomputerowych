using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindyTile : MonoBehaviour
{
    private PlayerControlls player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControlls>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            player.windyFloor = true;

        }
    }

}
