using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyIndicator : MonoBehaviour {

    public GameObject Empty;

    public GameObject Full;

    // Use this for initialization
    void Start () {
        GameObject keyInstance;
        keyInstance = Instantiate(Empty, transform.position, transform.rotation) as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider collider)
    {
        Destroy(gameObject);
    }
}
