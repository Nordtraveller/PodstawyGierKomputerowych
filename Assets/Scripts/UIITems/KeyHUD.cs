using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHUD : MonoBehaviour
{

    public Sprite[] Sprites;

    public Image KeyUI;

    public Image TeleportUI;

    public Image GoldenKeyUI;

    public Image TrapDestroyerUI;

    private PlayerStatus PlayerStatus;

    private float AlphaOfCollected = 1.0f;

    private float AlphaOfNotCollected = 0.1f;

    // Use this for initialization
    void Start ()
	{
	    PlayerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();

        TeleportUI.sprite = Sprites[2];
        GoldenKeyUI.sprite = Sprites[3];
        TrapDestroyerUI.sprite = Sprites[4];
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (PlayerStatus.haveKey)
	    {
	        KeyUI.sprite = Sprites[1];
	    }
	    else
	    {
	        KeyUI.sprite = Sprites[0];
        }

	    if (PlayerStatus.hasExtraKey)
	    {
            alphaSwapper(GoldenKeyUI, AlphaOfCollected);
	    }
	    else
	    {
	        alphaSwapper(GoldenKeyUI, AlphaOfNotCollected);
        }

	    if (PlayerStatus.hasExtraTeleport)
	    {
	        alphaSwapper(TeleportUI, AlphaOfCollected);
        }
	    else
	    {
	        alphaSwapper(TeleportUI, AlphaOfNotCollected);
        }
    }


    private void alphaSwapper(Image image, float alphaValue)
    {
        Color tmp = image.GetComponent<SpriteRenderer>().color;
        tmp.a = alphaValue;
        image.GetComponent<SpriteRenderer>().color = tmp;
    }
}
