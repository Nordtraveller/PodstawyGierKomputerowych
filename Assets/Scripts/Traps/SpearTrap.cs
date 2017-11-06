using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTrap : MonoBehaviour {

    private float startingY;
    private float attackToY;
    private float attackSpeed = 1f;

    private float attackRechargeTimmer;
    private float attackRechargeInterval = 1.5f;

    private bool kek = false;

    // Use this for initialization
    void Start () {
        startingY = transform.position.y;
        attackToY = transform.position.y + 0.9f;
    }
	
	// Update is called once per frame
	void Update () {

        /*   if (attackRechargeTimmer < attackRechargeInterval)
           {
               attackRechargeTimmer += Time.deltaTime;
           }
           else
           {
               attack();
               if (transform.position.y < startingY + 0.05f)
               {
                   this.transform.position.Set(this.transform.position.x, this.transform.position.y + 0.06f, this.transform.position.z);
                   attackRechargeTimmer = 0;

               }
           }*/
           attack();

    }


    void attack()
    {
        Vector3 start = new Vector3(this.transform.position.x, startingY, this.transform.position.z);
        Vector3 end = new Vector3(this.transform.position.x, attackToY, this.transform.position.z);
        float interpolant = Mathf.PingPong(Time.time * attackSpeed, 1f);
        this.transform.position = Vector3.Lerp(start, end, interpolant);
    }
}
