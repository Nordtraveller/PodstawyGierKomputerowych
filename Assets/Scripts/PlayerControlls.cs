using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    private Rigidbody body;
    private float distanceToGround;
    private RaycastHit hit;
    private float direction = 0;

    public bool onFire = false;
    public bool isBouncing = false;

    void Start ()
    {
        body = GetComponentInChildren<Rigidbody>();
        distanceToGround = GetComponentInChildren<CapsuleCollider>().bounds.extents.y;
    }

    void FixedUpdate ()
    {

        Vector3 position = transform.position;
        if (onFire)
        {
            position.x += direction * Time.deltaTime * GameMetrics.playerSpeed;
            if (Input.GetAxis("Horizontal") < 0) direction = -0.8f;
            if (Input.GetAxis("Horizontal") > 0) direction = 0.8f;
        }
        else
        {
            position.x += Input.GetAxis("Horizontal") * Time.deltaTime * GameMetrics.playerSpeed;
        }
        transform.position = position;


        if (IsGrounded() && isBouncing)
        {
            body.velocity += new Vector3(0f, GameMetrics.playerJumpForce, 0f);
        }
        if (Input.GetButton("Jump") && IsGrounded() && !isBouncing)
        {
            body.velocity += new Vector3(0f, GameMetrics.playerJumpForce, 0f);
        }
    }

    bool IsGrounded()
    {
        return (Physics.Raycast(body.transform.position, Vector3.down, out hit, distanceToGround + 0.1f) && hit.transform.tag == "Ground");
    }
}
