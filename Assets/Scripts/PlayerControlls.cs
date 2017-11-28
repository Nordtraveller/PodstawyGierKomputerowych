﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    private Rigidbody body;
    private float distanceToGround;
    private RaycastHit hit;
    private float direction = 0;
    private PlayerStatus playerStatus;
    private LevelCreator level;

    public bool fireFloor = false;
    public bool bouncyFloor = false;
    public bool windyFloor = false;
    private float jumpDelay = 0.02f;

    void Start ()
    {
        level = GameObject.FindWithTag("LevelCreator").GetComponent<LevelCreator>();
        playerStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
        body = GetComponentInChildren<Rigidbody>();
        distanceToGround = GetComponentInChildren<CapsuleCollider>().bounds.extents.y;
    }

    void Update ()
    {
        Vector3 position = transform.position;
        if (fireFloor)
        {
            if (Input.GetAxis("Horizontal") < 0) direction = -0.8f;
            if (Input.GetAxis("Horizontal") > 0) direction = 0.8f;
            position.x += direction * Time.deltaTime * GameMetrics.playerSpeed;
        }
        else if (windyFloor)
        {
            direction = -0.3f;
            position.x += direction * Time.deltaTime * GameMetrics.playerSpeed  
                           + Input.GetAxis("Horizontal") * Time.deltaTime * GameMetrics.playerSpeed;
        }
        else
        {
            position.x += Input.GetAxis("Horizontal") * Time.deltaTime * GameMetrics.playerSpeed;
        }
        transform.position = position;

        jumpDelay -= Time.deltaTime;
        if (IsGrounded() && bouncyFloor && jumpDelay < 0)
        {
            jumpDelay = 0.02f;
            body.velocity += new Vector3(0f, GameMetrics.playerJumpForce, 0f);
        }
        if (Input.GetButtonDown("Jump") && IsGrounded() && !bouncyFloor)
        {
            body.velocity += new Vector3(0f, GameMetrics.playerJumpForce, 0f);
        }

        //special keys
        if (Input.GetKeyDown(KeyCode.Q) && playerStatus.hasExtraKey)
        {
            playerStatus.haveKey = true;
            playerStatus.hasExtraKey = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && playerStatus.hasExtraTeleport)
        {            
            transform.position = new Vector3(level.actualFloor.exitTileNumber * 2, position.y, position.z); 
            playerStatus.hasExtraTeleport = false;
        }

		if (Input.GetKeyDown (KeyCode.E) && playerStatus.hasTrapDestroyer) 
		{
			
		}

    }

    bool IsGrounded()
    {
        return (Physics.Raycast(body.transform.position, Vector3.down, out hit, distanceToGround + 0.1f) && hit.transform.tag == "Ground");
    }
}
