using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkTile : MonoBehaviour {

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
           PlayerControlls player =  collision.gameObject.GetComponent<PlayerControlls>();
            player.darkFloor = true;
            player.dirLight.enabled = false;
            player.pointLight.enabled = true;
        }
    }
}
