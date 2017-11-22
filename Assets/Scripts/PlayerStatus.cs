using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool haveKey = false;
    public bool hasExtraKey = false;
    public bool hasExtraTeleport = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Trap")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Key")
        {
            haveKey = true;
        }
        if(other.tag == "ExtraKey")
        {
            hasExtraKey = true;
        }
        if (other.tag == "ExtraTeleport")
        {
            hasExtraTeleport = true;
        }
    }

}
