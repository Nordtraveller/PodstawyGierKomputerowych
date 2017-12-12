using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyTile : MonoBehaviour {

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (collision.collider.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerControlls>().bouncyFloor = true;
            }
        }
    }

}
