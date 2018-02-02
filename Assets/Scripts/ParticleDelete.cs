using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDelete : MonoBehaviour {

    Transform floor;
	void Start ()
    {
        floor = transform.parent.parent;
	}
	
	void Update ()
    {
		if(floor.position.y <= -1)
        {
            Destroy(this.gameObject);
        }
	}
}
