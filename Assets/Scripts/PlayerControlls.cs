using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    private Rigidbody body;
    private float distanceToGround;

    void Start ()
    {
        body = GetComponentInChildren<Rigidbody>();
        distanceToGround = GetComponentInChildren<CapsuleCollider>().bounds.extents.y;
    }
	
	void Update ()
    {
        Vector3 posistion = transform.position;
        posistion.x += Input.GetAxis("Horizontal") * Time.deltaTime * GameMetrics.playerSpeed;
        transform.position = posistion;
        //To poniżej się przyda do chodzenia po lodzie
        //body.AddForce(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * GameMetrics.playerSpeed, 0f, 0f), ForceMode.VelocityChange);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            body.AddForce(new Vector3(0f, GameMetrics.playerJumpForce, 0f), ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        return (Physics.Raycast(body.transform.position, Vector3.down, distanceToGround + 0.1f));
    }
}
