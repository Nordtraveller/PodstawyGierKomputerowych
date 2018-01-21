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

	private GameObject m_gameObjectPlane;
	private GameObject m_gameObjectPlane2;

	private float m_fCountdown = 3f;

    private void Awake()
    {
        CreateFirstFloor();
        player = GameObject.Find("Player").GetComponent<PlayerControlls>();
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        levelCounter = GameObject.Find("GameStatsCounter").GetComponent<GameStatsCounter>();

		audioSrc = GetComponent<AudioSource>();

		m_gameObjectPlane = GameObject.Find ("backgroundTexture");
		m_gameObjectPlane2 = GameObject.Find ("backgroundTexture2");

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
                new Vector3(0f, -GameMetrics.tileVerticalSize, 0f), (Time.time - startTime) / GameMetrics.dropDuration);

			// Background
			Color tmp = m_gameObjectPlane.GetComponent<MeshRenderer> ().material.color;
			tmp.a = (Time.time - startTime) / GameMetrics.dropDuration;
			tmp.a = 1 - tmp.a;
			m_gameObjectPlane.GetComponent<MeshRenderer> ().material.color = tmp;

			Color tmp2 = m_gameObjectPlane2.GetComponent<MeshRenderer> ().material.color;
			tmp2.a = (Time.time - startTime) / GameMetrics.dropDuration;

			m_gameObjectPlane2.GetComponent<MeshRenderer> ().material.color = tmp2;

            if(playerTriggerDrop)
            {
                player.transform.position = new Vector3(actualFloor.GetExitTileNumber() * GameMetrics.tileHorizontalSize
                , player.transform.position.y, 0.0f);
            }
            if (upperFloor.transform.position.y == 0f)
            {
                targetReached = true;
                playerTriggerDrop = false;
                if (previousFloor != null) Destroy(previousFloor.gameObject);
                previousFloor = actualFloor;
                actualFloor = upperFloor;
                upperFloor = newFloor;
                timeLeft = actualFloor.timeForFloor;
                ui_slider_timeLeft.maxValue = actualFloor.timeForFloor;
                previousFloor.DestroyItemsOnFloor();
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

        upperFloor = Instantiate(floorPrefabList[Random.Range(0, GameMetrics.floorsUnlocked)], new Vector3(0f, GameMetrics.upperFloorY, 0f),
            this.transform.rotation, this.transform);
        upperFloor.entranceTileNumber = exitTileNumber;
        upperFloor.CreateTiles();
        exitTileNumber = upperFloor.GetExitTileNumber();
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
    }

    void CreateNewFloor()
    {
        if(levelCounter.levelsPassedCount == unlockRate-1 && GameMetrics.floorsUnlocked < 3)
        {
            newFloor = Instantiate(floorPrefabList[2], new Vector3(0f, 2 * GameMetrics.upperFloorY, 0f),
            this.transform.rotation, this.transform);
            previousFloorIndex = 2;
        }
        else if (levelCounter.levelsPassedCount == (unlockRate * 2)-1 && GameMetrics.floorsUnlocked < 4)
        {
            newFloor = Instantiate(floorPrefabList[3], new Vector3(0f, 2 * GameMetrics.upperFloorY, 0f),
           this.transform.rotation, this.transform);
            previousFloorIndex = 3;
        }
        else if (levelCounter.levelsPassedCount == (unlockRate * 3)-1 && GameMetrics.floorsUnlocked < 5)
        {
            newFloor = Instantiate(floorPrefabList[4], new Vector3(0f, 2 * GameMetrics.upperFloorY, 0f),
           this.transform.rotation, this.transform);
            previousFloorIndex = 4;
        }
        else if (levelCounter.levelsPassedCount == (unlockRate * 4) - 1 && GameMetrics.floorsUnlocked < 6)
        {
            newFloor = Instantiate(floorPrefabList[5], new Vector3(0f, 2 * GameMetrics.upperFloorY, 0f),
           this.transform.rotation, this.transform);
            previousFloorIndex = 5;
        }
        else
        {
            int randomizedInt = previousFloorIndex;
            while (randomizedInt == previousFloorIndex)
            {
                randomizedInt = Random.Range(0, GameMetrics.floorsUnlocked);
            }
            previousFloorIndex = randomizedInt;
            newFloor = Instantiate(floorPrefabList[randomizedInt], new Vector3(0f, 2 * GameMetrics.upperFloorY, 0f),
           this.transform.rotation, this.transform);
        }
        newFloor.entranceTileNumber = exitTileNumber;
        newFloor.CreateTiles();
        exitTileNumber = newFloor.GetExitTileNumber();
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