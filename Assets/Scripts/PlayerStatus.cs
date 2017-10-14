﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool haveKey = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Trap")
        {
            Destroy(gameObject);
        }

        if(collision.collider.tag == "Key")
        {
            haveKey = true;
        }
    }

}
