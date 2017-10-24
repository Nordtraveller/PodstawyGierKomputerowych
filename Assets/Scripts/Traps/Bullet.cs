using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 bulletStartingPosition;

    public float bulletSpeed = 3.5f;

    // Use this for initialization
    void Start()
    {
        bulletStartingPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x - bulletSpeed * Time.deltaTime, transform.position.y,
            transform.position.z);

        if (bulletStartingPosition.x - transform.position.x > 6)
        {
            Destroy(gameObject);
        }
    }
}