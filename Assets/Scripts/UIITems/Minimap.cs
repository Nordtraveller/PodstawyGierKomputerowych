using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

	private float m_fPlayerPosition;
	private float m_fExitPosition;

	private PlayerStatus PlayerStatus;
	private LevelCreator level;
	private GameObject m_gameObjectPlayer;
	private float m_fGameObjectExitPosition;
	private float m_gameObjectKeyPosition;

	// Minimap items
	private GameObject m_spriteExitLocked;
	private GameObject m_spriteExitUnlocked;
	private GameObject m_spriteKey;
	private GameObject m_spritePlayer;

	RectTransform m_minimapRectTransform;

	// Use this for initialization
	void Start () {
		PlayerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
		level = GameObject.FindWithTag("LevelCreator").GetComponent<LevelCreator>();
		m_gameObjectPlayer = GameObject.FindWithTag ("Player");

	
		// sprites
		m_spriteExitLocked = transform.Find ("spriteExitLocked").gameObject;
		m_spriteExitUnlocked = transform.Find ("spriteExitUnlocked").gameObject;
		m_spriteKey = transform.Find ("spriteKey").gameObject;
		m_spritePlayer = transform.Find ("spritePlayer").gameObject;

		m_minimapRectTransform = GetComponent<RectTransform> ();

	}
	
	// Update is called once per frame
	void Update () {

		m_fGameObjectExitPosition = level.actualFloor.exitTileNumber * GameMetrics.tileSize;
		m_gameObjectKeyPosition = level.actualFloor.keyTileNumber * GameMetrics.tileSize;

		float fMinimapRectWidth = (m_minimapRectTransform.rect.width);


		// PLAYER
		// Update player position
		float fPlayerPositionX = m_gameObjectPlayer.transform.position.x;
		fPlayerPositionX /= (GameMetrics.floorSize * 2.0f);

		// Get rect for locked.
		Vector2 vPlayer = m_spritePlayer.GetComponent<RectTransform>().anchoredPosition;

		float fNewX = (fPlayerPositionX) * fMinimapRectWidth;
		fNewX -= fMinimapRectWidth * 0.5f;
		m_spritePlayer.GetComponent<RectTransform> ().anchoredPosition = new Vector2(fNewX, vPlayer.y);



		float fExitPositionX = m_fGameObjectExitPosition;
		fExitPositionX /= (GameMetrics.floorSize * 2.0f);


		if (PlayerStatus.haveKey) {
			// Don't show key and show unlocked exit

			m_spriteKey.SetActive (false);
			m_spriteExitLocked.SetActive (false);
			m_spriteExitUnlocked.SetActive (true);

			// show unlocked
			Vector2 vExit = m_spriteExitUnlocked.GetComponent<RectTransform>().anchoredPosition;

			fNewX = fExitPositionX * fMinimapRectWidth;
			fNewX -= fMinimapRectWidth * 0.5f;
			m_spriteExitUnlocked.GetComponent<RectTransform> ().anchoredPosition = new Vector2(fNewX, vExit.y);


		}
		else {
			m_spriteKey.SetActive (true);
			m_spriteExitLocked.SetActive (true);
			m_spriteExitUnlocked.SetActive (false);

			Vector2 vExit = m_spriteExitLocked.GetComponent<RectTransform>().anchoredPosition;

			fNewX = fExitPositionX * fMinimapRectWidth;
			fNewX -= fMinimapRectWidth * 0.5f;
		
			m_spriteExitLocked.GetComponent<RectTransform> ().anchoredPosition = new Vector2(fNewX, vExit.y);


			// Show key
			Vector2 vKey = m_spriteKey.GetComponent<RectTransform>().anchoredPosition;

			float fKeyPosX = m_gameObjectKeyPosition;
			fKeyPosX /= (GameMetrics.floorSize * 2.0f);

			fNewX = fKeyPosX * fMinimapRectWidth;
			fNewX -= fMinimapRectWidth * 0.5f;
			m_spriteKey.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (fNewX, vKey.y);


		}
		
	}
}
