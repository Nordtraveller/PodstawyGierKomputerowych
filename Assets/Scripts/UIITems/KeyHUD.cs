﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHUD : MonoBehaviour
{

    public Sprite[] Sprites;

    public Image[] Letters;

    public Image KeyUI;

    public Image TeleportUI;

    public Image GoldenKeyUI;

    public Image TrapDestroyerUI;

    private PlayerStatus PlayerStatus;

    private float AlphaOfCollected = 1.0f;

    private float AlphaOfNotCollected = 0.4f;

    // Use this for initialization
    void Start ()
	{
	    PlayerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
	    KeyUI.enabled = false;
	    //TeleportUI.sprite = Sprites[2];
	    //GoldenKeyUI.sprite = Sprites[3];
	    //TrapDestroyerUI.sprite = Sprites[4];
	}
	
	// Update is called once per frame
	void Update ()
	{
/*	    if (PlayerStatus.haveKey)
	    {
	        KeyUI.sprite = Sprites[1];
	    }
	    else
	    {
	        KeyUI.sprite = Sprites[0];
        }*/

	    if (PlayerStatus.hasExtraKey)
	    {
            alphaSwapper(GoldenKeyUI, AlphaOfCollected);
            Letters[0].GetComponent<Image>().color = Color.green;
        }
	    else
	    {
	        alphaSwapper(GoldenKeyUI, AlphaOfNotCollected);
	        Letters[0].GetComponent<Image>().color = Color.white;
        }

	    if (PlayerStatus.hasExtraTeleport)
	    {
	        alphaSwapper(TeleportUI, AlphaOfCollected);
	        Letters[1].GetComponent<Image>().color = Color.green;
        }
	    else
	    {
	        alphaSwapper(TeleportUI, AlphaOfNotCollected);
	        Letters[1].GetComponent<Image>().color = Color.white;
        }

		if (PlayerStatus.hasTrapDestroyer)
		{
			alphaSwapper (TrapDestroyerUI, AlphaOfCollected);
		    Letters[2].GetComponent<Image>().color = Color.green;
        } 
		else
		{
			alphaSwapper (TrapDestroyerUI, AlphaOfNotCollected);
		    Letters[2].GetComponent<Image>().color = Color.white;
        }

    }


    private void alphaSwapper(Image image, float alphaValue)
    {
        Color tmp = image.GetComponent<Image>().color;
        tmp.a = alphaValue;
        image.GetComponent<Image>().color = tmp;
    }
}
