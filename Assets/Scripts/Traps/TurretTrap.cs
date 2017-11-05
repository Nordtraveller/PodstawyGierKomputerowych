using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTrap : MonoBehaviour
{
    public bool currentFloor = false; //strzela tylko jeśli jest na aktywnym florze

    public bool shootingLeft = true; // strzela w lewo

    public bool reloadingAmmo = false; //czy przeładowuje

    public float reloadingTimmer;

    public float reloadInterval;

    public GameObject bullet; //pocisk


    // Use this for initialization
    void Start()
    {
        reloadInterval = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        FloorCheck();
        if (currentFloor && !reloadingAmmo)
        {
            shoot();
        }
        reloadingCheck();
    }

    void FloorCheck()
    {
        if (transform.parent.position.y <= 2.0f)
        {
            currentFloor = true;
        }
        else
        {
            currentFloor = false;
        }
    }

    void reloadingCheck()
    {
        reloadingTimmer += Time.deltaTime;
        if (reloadingTimmer >= reloadInterval)
        {
            reloadingAmmo = false;
        }
        else
        {
            reloadingAmmo = true;
        }
    }


    void shoot()
    {
        Vector3 bulletStartingPosition =
            new Vector3(transform.position.x - 0.7f, transform.position.y + 0.9f, transform.position.z);
        GameObject bulletInstance;
        bulletInstance = Instantiate(bullet, bulletStartingPosition, transform.rotation) as GameObject;
        reloadingAmmo = true;
        reloadingTimmer = 0;
    }
}