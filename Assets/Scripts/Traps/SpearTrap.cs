using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTrap : MonoBehaviour {

    private float startingY;
    private float attackToY;
    private float attackSpeed = 4f;

    private float attackRechargeTimmer;
    private float attackRechargeInterval = 2.0f;

    private bool recharge = false;

    // Use this for initialization
    void Start () {
        startingY = transform.position.y;
        attackToY = transform.position.y + 1.1f;
    }

	// Update is called once per frame
	void Update ()
	{

	    attackRechargeTimmer += Time.deltaTime;

	    if (!recharge && attackRechargeTimmer >= attackRechargeInterval)
	    {
	        Attack();
	    }
	    if (recharge)
	    {
	        PullBack();
	    }

    }


    void Attack()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + attackSpeed * Time.deltaTime,
            transform.position.z);
        if (transform.position.y > attackToY)
        {
            transform.position = new Vector3(transform.position.x, attackToY, transform.position.z);
            recharge = true;
        }
    }


    void PullBack()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - attackSpeed * Time.deltaTime,
            transform.position.z);
        if (transform.position.y < startingY)
        {
            transform.position = new Vector3(transform.position.x, startingY, transform.position.z);
            recharge = false;
            attackRechargeTimmer = 0;
        }
    }
}
