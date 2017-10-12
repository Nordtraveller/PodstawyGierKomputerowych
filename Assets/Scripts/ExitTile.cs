using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTile : MonoBehaviour
{
    private LevelCreator creator;
    private bool triggered = false;

    private void Awake()
    {
        creator = GetComponentInParent<LevelCreator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!triggered || other.tag == "Player")
        {
            triggered = true;
            Debug.Log("Wychodzimy");
            creator.DropUpperFloor();
        }
    }

}
