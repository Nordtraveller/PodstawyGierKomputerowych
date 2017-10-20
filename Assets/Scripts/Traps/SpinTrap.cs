using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTrap : MonoBehaviour {
	
	void Update ()
    {
        this.transform.Rotate(new Vector3(0.0f, 0.0f, 30.0f * Time.deltaTime));
	}
}
