using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyTile : MonoBehaviour {

    private PlayerControlls player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControlls>();
    }

}
