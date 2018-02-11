using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool haveKey = false;
    public bool hasExtraKey = false;
    public bool hasExtraTeleport = false;
	public bool hasTrapDestroyer = false;
    public FloorType actualFloorType = FloorType.Default;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Trap")
        {
			if (hasTrapDestroyer)
			{
				Destroy (collision.gameObject);
				hasTrapDestroyer = false;
			} 
			else 
			{
				Destroy (gameObject);
			}
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Key")
        {
            haveKey = true;
        }
        if(other.tag == "ExtraKey")
        {
            hasExtraKey = true;
        }
        if (other.tag == "ExtraTeleport")
        {
            hasExtraTeleport = true;
		}
		if (other.tag == "powerupTrapDestroyer")
		{
			hasTrapDestroyer = true;
		}
    }
}
