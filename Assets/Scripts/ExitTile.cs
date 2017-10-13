using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTile : MonoBehaviour
{
    private LevelCreator creator;

    private void Awake()
    {
        creator = GetComponentInParent<LevelCreator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            creator.DropUpperFloor();
            Destroy(this);
        }
    }

}
