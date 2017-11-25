﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyIndicator : MonoBehaviour {

    public GameObject Empty;

    public GameObject Full;

    private GameObject keyEmptyInstance;

    private GameObject keyFullInstance;

    private PlayerStatus PlayerStatus;

    private LevelCreator level;

    private float emptyCreateTimmer;

    private float emptyCreateInterval = 3.0f;

    

    // Use this for initialization
    void Start () {
        PlayerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        level = GameObject.FindWithTag("LevelCreator").GetComponent<LevelCreator>();

        keyEmptyInstance = Instantiate(Empty, transform.position, transform.rotation) as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
	    //emptyCreateTimmer += Time.deltaTime;

        if (PlayerStatus.haveKey && keyFullInstance == null)
	    {
            Destroy(keyEmptyInstance);
	        keyFullInstance = Instantiate(Full, transform.position, transform.rotation) as GameObject;
	        //emptyCreateTimmer = 0.0f;
	    }
	    else if(!PlayerStatus.haveKey && keyFullInstance != null)
	    {
            Destroy(keyFullInstance);
	        //emptyCreateTimmer = 0.0f;
            
        }
        else if (level.getUpperFloor().transform.position.y <= 0.5 && keyEmptyInstance == null)
        {
            keyEmptyInstance = Instantiate(Empty, transform.position, transform.rotation) as GameObject;
        }
    }

    private void OnDestroy()
    {
        if(keyFullInstance != null)
        {
            Destroy(keyFullInstance);
        }

        if(keyEmptyInstance != null)
        {
            Destroy(keyEmptyInstance);
        }
    }


    /*    private void OnTriggerEnter(Collider collider)
        {
            Destroy(gameObject);
        }*/
}
