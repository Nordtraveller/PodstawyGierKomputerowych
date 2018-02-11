using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCreator : MonoBehaviour
{
    public Floor[] floorPrefabList;
    public ExitTile exitPrefab;
    public FloorEntranceTile entrancePrefab;
    public Text ui_text_timeLeft;
    public GameObject ui_text_floorUnlocked;
    public Slider ui_slider_timeLeft;

	public Texture2D textureDefaultFloor;
	public Texture2D textureFireFloor;
	public Texture2D textureWindyFloor;
	public Texture2D textureDarkFloor;
	public Texture2D textureBouncyFloor;
	public Texture2D textureChangingLightFloor;
	public Texture2D textureCosmicFloor;

    private int exitTileNumber;
    private Floor newFloor;
    private Floor upperFloor;
    public Floor actualFloor;
    private Floor previousFloor;
    private bool targetReached = true;
    public bool playerTriggerDrop = false;
    private float startTime;
    private float timeLeft;
    private PlayerControlls player;
    private CameraController cameraController;
    private GameStatsCounter levelCounter;
    private int previousFloorIndex = 0;

    private float timeDeltaOneSecondInterval = 0.0f;
	private float timeToTen = 0.0f;
    private int unlockRate = 5;

	public AudioClip clockTickAudioClip;
	private AudioSource audioSrc;

	private GameObject m_backgroundCurrentFloor;
	private GameObject m_backgroundNewFloor;
	private bool   m_bFlip = false;

	private float m_fCountdown = 3f;

    private void Awake()
    {
		Debug.Log ("AWAKE!!!!");

		m_backgroundCurrentFloor = GameObject.Find ("backgroundTextureCurrent");
		m_backgroundNewFloor = GameObject.Find ("backgroundTextureUpper");

		// make sure only current floor background is visible
		{
			// A = 1.0 - fully visible
			// A = 0.0 - not visible

			// current floor with 1
			Color tmp2 = m_backgroundCurrentFloor.GetComponent<MeshRenderer> ().material.color;
			tmp2.a = 1;

			m_backgroundCurrentFloor.GetComponent<MeshRenderer> ().material.color = tmp2;

			// upper floor with 0
			Color tmp = m_backgroundNewFloor.GetComponent<MeshRenderer> ().material.color;
			tmp.a = 0;

			m_backgroundNewFloor.GetComponent<MeshRenderer> ().material.color = tmp;


		}
	


        CreateFirstFloor();
        player = GameObject.Find("Player").GetComponent<PlayerControlls>();
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        levelCounter = GameObject.Find("GameStatsCounter").GetComponent<GameStatsCounter>();

		audioSrc = GetComponent<AudioSource>();





    }


    private void Update()
    {
        UnlockFloors();
        if(player == null)
        {			
            m_fCountdown -= Time.deltaTime;
			int nDelta = (int)m_fCountdown;

            if (Input.GetButtonDown("Reset"))
            {
                m_fCountdown = -1;
            }

            StartCoroutine(ShowMessage("Press \"r\" for instant reset \n Restarting game in " + (nDelta+1)));
	
			if (m_fCountdown < 0)
			{
				m_fCountdown = 3;
				DropUpperFloor();
				cameraController.restartGame ();
				m_bFlip = false;
			} 
			else 
			{
				return;
			}            
        }


        if (!targetReached)
        {
            newFloor.transform.position = Vector3.Lerp(new Vector3(0f, 2 * GameMetrics.upperFloorY, 0f),
                 new Vector3(0f, GameMetrics.upperFloorY, 0f), (Time.time - startTime) / GameMetrics.dropDuration);
            upperFloor.transform.position = Vector3.Lerp(new Vector3(0f, GameMetrics.upperFloorY, 0f),
                Vector3.zero, (Time.time - startTime) / GameMetrics.dropDuration);
            actualFloor.transform.position = Vector3.Lerp(Vector3.zero,
                new Vector3(0f, -GameMetrics.tileSize, 0f), (Time.time - startTime) / GameMetrics.dropDuration);

			// Background
			{
				// zamienić wartości 0-1



				Color tmp = m_backgroundCurrentFloor.GetComponent<MeshRenderer> ().material.color;
				tmp.a = (Time.time - startTime) / GameMetrics.dropDuration;
				if (m_bFlip)
					tmp.a = 1 - tmp.a;
				m_backgroundCurrentFloor.GetComponent<MeshRenderer> ().material.color = tmp;


				//Debug.Log ("TMP1" + tmp.a);

				Color tmp2 = m_backgroundNewFloor.GetComponent<MeshRenderer> ().material.color;
				tmp2.a = (Time.time - startTime) / GameMetrics.dropDuration;
				if (!m_bFlip)
					tmp2.a = 1 - tmp2.a;

				m_backgroundNewFloor.GetComponent<MeshRenderer> ().material.color = tmp2;
				//Debug.Log ("TMP2" + tmp2.a);


			}



            if(playerTriggerDrop)
            {
                player.transform.position = new Vector3(actualFloor.GetExitTileNumber() * GameMetrics.tileSize
                , player.transform.position.y, 0.0f);
            }
			if (upperFloor.transform.position.y == 0f) {
				targetReached = true;
				playerTriggerDrop = false;
				if (previousFloor != null)
					Destroy (previousFloor.gameObject);



				// po pierwszych akcjach tutaj, currentFloor ma wartość A=0 (niewidoczny)
				// a upperFloor ma wartość A = 1 (w pełni widoczny).
				// 
				// Teraz możemy załadować nową górną teksturę do currentFloor i zamienić flipy




				previousFloor = actualFloor;
				actualFloor = upperFloor;
				upperFloor = newFloor;
				timeLeft = actualFloor.timeForFloor;
				ui_slider_timeLeft.maxValue = actualFloor.timeForFloor;
				previousFloor.DestroyItemsOnFloor ();

				//Debug.Log ("AAAAAXD");

				GameObject go;
				if (m_bFlip) {
					go = m_backgroundCurrentFloor;
				} else
					go = m_backgroundNewFloor;

				switch (previousFloorIndex) {
				// default floor
				case 0:
					{
						go.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureDefaultFloor);
						break;
					}

				// fire floor
				case 1:
					{
						go.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureFireFloor);
						break;
					}

				// windy floor
				case 2:
					{
						go.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureWindyFloor);
						break;
					}

				// dark floor
				case 3:
					{
						go.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureDarkFloor);
						break;
					}

				// bouncy floor
				case 4:
					{
						go.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureBouncyFloor);
						break;
					}

				// changing light/cosmos floor
				case 5:
				case 6:
					{
						go.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureFireFloor);
						break;
					}
				}
			}



			timeToTen = 0.0f;
        }
        else
        {
            timeLeft -= Time.deltaTime;

            int nTimeLeft = (int)timeLeft;

			timeDeltaOneSecondInterval += Time.deltaTime;

			if (timeDeltaOneSecondInterval >= 1.0f) 
			{	
				if (nTimeLeft < 10)
				{
					audioSrc.PlayOneShot (clockTickAudioClip, 1.0f);
				}
				if (nTimeLeft <= 5)
				{
					audioSrc.PlayDelayed (0.5f);
				}

				timeDeltaOneSecondInterval = 0.0f;
			}

			if (nTimeLeft < 10)
			{
				timeToTen += Time.deltaTime;


				int size = upperFloor.GetTilesObjectList ().Count;
				for (int i = 0; i < size; i++) {
					GameObject tile = upperFloor.GetTilesObjectList () [i];

					float param = (timeToTen / 100.0f);
					float offsetY = param * Mathf.Sin (timeToTen * 10.14f);

					Vector3 oldPos = new Vector3 (tile.transform.position.x, tile.transform.position.y, tile.transform.position.z);

					//oldPos.x += param;
					oldPos.y += offsetY;

					tile.transform.position = oldPos;

				}
			}

			
			ui_text_timeLeft.text = "Time Left: " + nTimeLeft.ToString();
            ui_slider_timeLeft.value = timeLeft;

            if (timeLeft < 0)
            {
                DropUpperFloor();
                ui_text_timeLeft.text = "Game Over";
                cameraController.restartGame();
            }
        }
    }

    void CreateFirstFloor()
    {
		actualFloor = Instantiate(floorPrefabList[0], Vector3.zero, this.transform.rotation, this.transform);
        actualFloor.entranceTileNumber = -1;
        actualFloor.CreateTiles();
        exitTileNumber = actualFloor.GetExitTileNumber();
        timeLeft = actualFloor.timeForFloor;
        ui_slider_timeLeft.maxValue = actualFloor.timeForFloor;

		int nextFloorIndex = Random.Range (0, GameMetrics.floorsUnlocked);

		upperFloor = Instantiate(floorPrefabList[nextFloorIndex], new Vector3(0f, GameMetrics.upperFloorY, 0f),
            this.transform.rotation, this.transform);
        upperFloor.entranceTileNumber = exitTileNumber;
        upperFloor.CreateTiles();
        exitTileNumber = upperFloor.GetExitTileNumber();


		m_backgroundCurrentFloor.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureDefaultFloor);

		switch (nextFloorIndex) {
		// default floor
		case 0:
			{
				m_backgroundNewFloor.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureDefaultFloor);
				break;
			}

		// fire floor
		case 1:
			{
				m_backgroundNewFloor.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureFireFloor);
				break;
			}

		// windy floor
		case 2:
			{
				m_backgroundNewFloor.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureWindyFloor);
				break;
			}

		// dark floor
		case 3:
			{
				m_backgroundNewFloor.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureDarkFloor);
				break;
			}

		// bouncy floor
		case 4:
			{
				m_backgroundNewFloor.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureBouncyFloor);
				break;
			}

		// changing light/cosmos floor
		case 5:
		case 6:
			{
				m_backgroundNewFloor.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureFireFloor);
				break;
			}
		}



    }

    void UnlockFloors()
    {
        if (levelCounter.levelsPassedCount == unlockRate && GameMetrics.floorsUnlocked < 3 )
        {
            GameMetrics.floorsUnlocked = 3;
            StartCoroutine(ShowMessage("New floor unlocked! Be careful to not be blown away!"));
        }
        if (levelCounter.levelsPassedCount == (unlockRate*2) && GameMetrics.floorsUnlocked < 4)
        {
            GameMetrics.floorsUnlocked = 4;
            StartCoroutine(ShowMessage("New floor unlocked! It can get really dark now. "));
        }
        if (levelCounter.levelsPassedCount == (unlockRate * 3) && GameMetrics.floorsUnlocked < 5)
        {
            GameMetrics.floorsUnlocked = 5;
            StartCoroutine(ShowMessage("New floor unlocked! Chew you gum!"));
        }
        if (levelCounter.levelsPassedCount == (unlockRate * 4) && GameMetrics.floorsUnlocked < 6)
        {
            GameMetrics.floorsUnlocked = 6;
            StartCoroutine(ShowMessage("New floor unlocked! Epilepsy!"));
        }
        if (levelCounter.levelsPassedCount == (unlockRate * 5) && GameMetrics.floorsUnlocked < 7)
        {
            GameMetrics.floorsUnlocked = 7;
            StartCoroutine(ShowMessage("New floor unlocked! Travel to the Moon!"));
        }
    }

    void CreateNewFloor()
    {
		//Debug.Log ("start: previousFloorIndex = " + previousFloorIndex);
		/*
		switch (previousFloorIndex)
		{
		// default floor
		case 0:
			{
				m_backgroundCurrentFloor.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureDefaultFloor);
				break;
			}

		// fire floor
		case 1:
			{
				m_backgroundCurrentFloor.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureFireFloor);
				break;
			}

		// windy floor
		case 2:
			{
				m_backgroundCurrentFloor.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureWindyFloor);
				break;
			}

		// dark floor
		case 3:
			{
				m_backgroundCurrentFloor.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureDarkFloor);
				break;
			}

		// bouncy floor
		case 4:
			{
				m_backgroundCurrentFloor.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureBouncyFloor);
				break;
			}

		// changing light/cosmos floor
		case 5:
		case 6:
			{
				m_backgroundCurrentFloor.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", textureFireFloor);
				break;
			}
		}
		*/

		// z górnego na aktualne


		if (levelCounter.levelsPassedCount == unlockRate - 1 && GameMetrics.floorsUnlocked < 3) 
		{
				newFloor = Instantiate (floorPrefabList [2], new Vector3 (0f, 2 * GameMetrics.upperFloorY, 0f),
					this.transform.rotation, this.transform);
				previousFloorIndex = 2;
		}

		else if (levelCounter.levelsPassedCount == (unlockRate * 2) - 1 && GameMetrics.floorsUnlocked < 4)
		{
				newFloor = Instantiate (floorPrefabList [3], new Vector3 (0f, 2 * GameMetrics.upperFloorY, 0f),
					this.transform.rotation, this.transform);
				previousFloorIndex = 3;
		}
		else if (levelCounter.levelsPassedCount == (unlockRate * 3) - 1 && GameMetrics.floorsUnlocked < 5) 
		{
				newFloor = Instantiate (floorPrefabList [4], new Vector3 (0f, 2 * GameMetrics.upperFloorY, 0f),
					this.transform.rotation, this.transform);
				previousFloorIndex = 4;
		} 
		else if (levelCounter.levelsPassedCount == (unlockRate * 4) - 1 && GameMetrics.floorsUnlocked < 6) 
		{
				newFloor = Instantiate (floorPrefabList [5], new Vector3 (0f, 2 * GameMetrics.upperFloorY, 0f),
					this.transform.rotation, this.transform);
				previousFloorIndex = 5;
		} 
		else if (levelCounter.levelsPassedCount == (unlockRate * 5) - 1 && GameMetrics.floorsUnlocked < 7) 
		{
				newFloor = Instantiate (floorPrefabList [6], new Vector3 (0f, 2 * GameMetrics.upperFloorY, 0f),
					this.transform.rotation, this.transform);
				previousFloorIndex = 6;
		} 
		else 
		{
			int randomizedInt = previousFloorIndex;
			while (randomizedInt == previousFloorIndex)
			{
					randomizedInt = Random.Range (0, GameMetrics.floorsUnlocked);
			}


			previousFloorIndex = randomizedInt;

			newFloor = Instantiate (floorPrefabList [randomizedInt], new Vector3 (0f, 2 * GameMetrics.upperFloorY, 0f),
					this.transform.rotation, this.transform);
		}

		newFloor.entranceTileNumber = exitTileNumber;
		newFloor.CreateTiles ();
		exitTileNumber = newFloor.GetExitTileNumber ();

		Debug.Log ("koniec: previousFloorIndex = " + previousFloorIndex);

		m_bFlip = !m_bFlip;
    }

    public void DropUpperFloor()
    {

        CreateNewFloor();

        int size = upperFloor.GetTilesObjectList ().Count;
		for (int i = 0; i < size; i++) {
			GameObject tile = upperFloor.GetTilesObjectList () [i];

			//float param = (timeToTen / 100.0f);
			//float offsetY = param * Mathf.Sin (timeToTen * 10.14f);

			Vector3 oldPos = new Vector3 (tile.transform.localPosition.x, tile.transform.localPosition.y, tile.transform.localPosition.z);
		
			//oldPos.x += param;
			oldPos.y = 0;

			tile.transform.localPosition = oldPos;

		}


        startTime = Time.time;
        targetReached = false;
    }

    IEnumerator ShowMessage(string msg)
    {
        ui_text_floorUnlocked.GetComponent<Text>().text = msg;
        ui_text_floorUnlocked.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        ui_text_floorUnlocked.SetActive(false);
    }


    public Floor getUpperFloor()
    {
        return upperFloor;
    }
}