﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyIndicator : MonoBehaviour {





	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider collider)
    {
        Destroy(gameObject);
    }
}