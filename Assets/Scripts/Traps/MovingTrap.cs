using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingTrap : MonoBehaviour
{
    private float startingX;
    private float randomStartTime;
    private float x;
    private float speed = 1f;
    private float time;

    private void Start()
    {
        startingX = transform.position.x;
    }

    void Update()
    {
            Vector3 pointA = new Vector3(startingX, this.transform.position.y, this.transform.position.z);
            Vector3 pointB = pointA + new Vector3((GetComponentInParent<Trap>().size - GameMetrics.tileHorizontalSize), 0, 0);
            float pingPong = Mathf.PingPong(Time.time * speed, 1);
            this.transform.position = Vector3.Lerp(pointA, pointB, pingPong);

            
    }
}

