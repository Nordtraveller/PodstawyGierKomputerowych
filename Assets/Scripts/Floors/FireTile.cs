using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTile : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControlls>().fireFloor = true;
        }
    }

}
