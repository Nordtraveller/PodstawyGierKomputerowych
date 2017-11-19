using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHUD : MonoBehaviour
{

    public Sprite[] Keys;

    public Image KeyUI;

    private PlayerStatus PlayerStatus;

	// Use this for initialization
	void Start ()
	{
	    PlayerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (PlayerStatus.haveKey)
	    {
	        KeyUI.sprite = Keys[1];
	    }
	    else
	    {
	        KeyUI.sprite = Keys[0];
        }
	}
}
