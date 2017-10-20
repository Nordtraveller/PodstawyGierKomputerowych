using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingTrap : MonoBehaviour
{
    private float startingX;
    private float randomStartTime;
    private float speed = 1f;
    private float time;

    private void Start()
    {
        startingX = transform.position.x + 1;
        randomStartTime = Random.Range(0.0f, 0.5f);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= randomStartTime)
        {
            Vector3 pointA = new Vector3(startingX, this.transform.position.y, this.transform.position.z);
            Vector3 pointB = pointA + new Vector3((GetComponentInParent<Trap>().size - 1), 0, 0);
            float pingPong = Mathf.PingPong(Time.time * speed, 1);
            this.transform.position = Vector3.Lerp(pointA, pointB, pingPong);
        }
            
    }
}

