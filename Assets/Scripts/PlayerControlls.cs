using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    private Rigidbody body;
    private float distanceToGround;
    private RaycastHit hit;

    void Start ()
    {
        body = GetComponentInChildren<Rigidbody>();
        distanceToGround = GetComponentInChildren<CapsuleCollider>().bounds.extents.y;
    }
	
	void FixedUpdate ()
    {
        Vector3 posistion = transform.position;
        posistion.x += Input.GetAxis("Horizontal") * Time.deltaTime * GameMetrics.playerSpeed;
        transform.position = posistion;
        if (Input.GetButton("Jump") && IsGrounded())
        {
            //body.AddForce(new Vector3(0f, GameMetrics.playerJumpForce, 0f), ForceMode.Impulse);
            body.velocity += new Vector3(0, GameMetrics.playerJumpForce, 0);
        }
    }

    bool IsGrounded()
    {
        return (Physics.Raycast(body.transform.position, Vector3.down, out hit, distanceToGround + 0.1f) && hit.transform.tag == "Ground");
    }
}
