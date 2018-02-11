﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    private Rigidbody2D body;
    private float distanceToGround;
    private RaycastHit2D hit;
    private float direction = 0;
    private float jumpForce = 0;
    private PlayerStatus playerStatus;
    private LevelCreator level;
    public LayerMask groundLayer;

    public Light dirLight;
    public Light pointLight;
    private float jumpDelay = 0.02f;
    private float previousTime = 0f;
    private float newTime = 0f;
    private bool directionalLight = false;

    public GameObject shield;

    void Start ()
    {
        level = GameObject.FindWithTag("LevelCreator").GetComponent<LevelCreator>();
        playerStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
        dirLight = GameObject.FindWithTag("Directional light").GetComponent<Light>();
        pointLight = GameObject.FindWithTag("Point light").GetComponent<Light>();
        body = GetComponentInChildren<Rigidbody2D>();
        distanceToGround = GetComponentInChildren<CapsuleCollider2D>().bounds.extents.y;
    }

    void Update ()
    {
        newTime += Time.deltaTime;
        if((previousTime + 0.75f) < newTime)
        {
            previousTime = newTime;
            directionalLight = !directionalLight;
        }

        switch(playerStatus.actualFloorType) //move case
        {
            case FloorType.Fire:
                if (Input.GetAxis("Horizontal") < 0) direction = -0.8f;
                if (Input.GetAxis("Horizontal") > 0) direction = 0.8f;
                break;

            case FloorType.Windy:
                direction = Input.GetAxis("Horizontal") - 0.3f;
                break;

            default:
                direction = Input.GetAxis("Horizontal");
                break;
        }
        switch(playerStatus.actualFloorType) //light case
        {
            case FloorType.Dark:
                dirLight.enabled = false;
                pointLight.enabled = true;
                break;

            case FloorType.Blink:
                dirLight.enabled = directionalLight;
                break;

            default:
                dirLight.enabled = true;
                pointLight.enabled = false;
                break;
        }
        jumpForce = 0.0f;
        switch (playerStatus.actualFloorType) //jump case
        {
            case FloorType.Bouncy:
                jumpDelay -= Time.deltaTime;
                if (IsGrounded() && jumpDelay < 0)
                {
                    jumpDelay = 0.02f;
                    jumpForce = GameMetrics.playerJumpForce;
                }
                break;

            case FloorType.Cosmic:
                if (Input.GetButtonDown("Jump") && IsGrounded())
                {
                    jumpForce = GameMetrics.playerJumpForce * 1.4f;
                }
                break;

            default:
                if (Input.GetButtonDown("Jump") && IsGrounded())
                {
                    jumpForce = GameMetrics.playerJumpForce;
                }
                break;
        }
        MovePlayer(direction, jumpForce);

        Vector3 position = transform.position;
        //special keys
        if (Input.GetKeyDown(KeyCode.Q) && playerStatus.hasExtraKey)
        {
            playerStatus.haveKey = true;
            playerStatus.hasExtraKey = false;
        }
        if (Input.GetKeyDown(KeyCode.W) && playerStatus.hasExtraTeleport)
        {            
            transform.position = new Vector3(level.actualFloor.exitTileNumber * GameMetrics.tileSize, position.y + GameMetrics.tileSize, position.z); 
            playerStatus.hasExtraTeleport = false;
        }
        if (playerStatus.hasTrapDestroyer)
        {
            shield.SetActive(true);
            shield.transform.position = new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z);
        }
        if (!playerStatus.hasTrapDestroyer)
        {
            shield.SetActive(false);
        }
    }

    void MovePlayer(float direction, float jumpForce)
    {
        Vector3 position = transform.position;
        position.x += direction * Time.deltaTime * GameMetrics.playerSpeed;
        transform.position = position;
        body.velocity += new Vector2(0f, jumpForce);
    }

    bool IsGrounded()
    {
        hit = Physics2D.Raycast(body.transform.position, Vector2.down, distanceToGround + 0.1f, groundLayer);
        return (hit.transform.tag == "Ground");
    }
}
