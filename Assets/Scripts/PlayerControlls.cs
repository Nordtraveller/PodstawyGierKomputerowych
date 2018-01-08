using System.Collections;
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
    private Animator animator;

    public Light dirLight;
    public Light pointLight;
    public bool fireFloor = false;
    public bool bouncyFloor = false;
    public bool windyFloor = false;
    public bool darkFloor = false;
    public bool cosmicFloor = false;
    public bool changingLightFloor = false;
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
        body = GetComponentInChildren<Rigidbody>();
        distanceToGround = GetComponentInChildren<CapsuleCollider>().bounds.extents.y;
        animator = GetComponentInChildren<Animator>();
    }

    void Update ()
    {
        newTime += Time.deltaTime;
        if((previousTime + 0.75f) < newTime)
        {
            previousTime = newTime;
            directionalLight = !directionalLight;
        }
        Vector3 position = transform.position;
        pointLight.transform.position = new Vector3(position.x + 1.0f, position.y, position.z);
        if(cosmicFloor)
        {
            // ???
        }
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
        if (Input.GetAxis("Horizontal") < 0)
        {
            animator.SetBool("IsMoveLeft", true);
            animator.SetBool("IsMoveRight", false);
            //animator.SetBool("IsFacingLeft", true);
            //animator.SetBool("IsFacingRight", false);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            animator.SetBool("IsMoveLeft", false);
            animator.SetBool("IsMoveRight", true);
            //animator.SetBool("IsFacingLeft", false);
            //animator.SetBool("IsFacingRight", true);
        }
        else if(!fireFloor)
        {
            animator.SetBool("IsMoveLeft", false);
            animator.SetBool("IsMoveRight", false);
        }
        transform.position = position;


        if (level.getUpperFloor().tilePrefab.name == "DarkTile" && level.getUpperFloor().transform.position.y <= 3.0f)
        {
            darkFloor = true;
        }
        if (darkFloor)
        {
            dirLight.enabled = false;
            pointLight.enabled = true;
        }
        if (!darkFloor)
        {
            dirLight.enabled = true;
            pointLight.enabled = false;
        }

        if(changingLightFloor)
        {
            dirLight.enabled = directionalLight;
        }

        jumpDelay -= Time.deltaTime;
        if(bouncyFloor)
        {
            if (IsGrounded() && jumpDelay < 0)
            {
                jumpDelay = 0.02f;
                body.velocity += new Vector3(0f, GameMetrics.playerJumpForce, 0f);
                animator.SetBool("IsJumping", true);
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && IsGrounded() && !bouncyFloor)
            {
                body.velocity += new Vector3(0f, GameMetrics.playerJumpForce, 0f);
                animator.SetBool("IsJumping", true);
            }
            else if (IsGrounded())
            {
                animator.SetBool("IsJumping", false);
            }
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

        //shield
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



    bool IsGrounded()
    {
        return (Physics.Raycast(body.transform.position, Vector3.down, out hit, distanceToGround + 0.1f) && hit.transform.tag == "Ground");
    }
}
