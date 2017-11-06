using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDelete : MonoBehaviour {

    Transform floor;
	// Use this for initialization
	void Start ()
    {
        floor = transform.parent.parent;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(floor.position.y <= -1)
        {
            Destroy(this.gameObject);
        }
	}
}
