using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingLightTile : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            PlayerControlls player = collision.gameObject.GetComponent<PlayerControlls>();
            player.changingLightFloor = true;
        }
    }
}
