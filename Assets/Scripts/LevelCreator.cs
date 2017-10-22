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

    private int exitTileNumber;
    private Floor newFloor;
    private Floor upperFloor;
    private Floor actualFloor;
    private Floor previousFloor;
    private bool targetReached = true;
    private float startTime;
    private float timeLeft;
    private PlayerControlls player;

    private void Awake()
    {
        CreateFirstFloor();
        player = GameObject.Find("Player").GetComponent<PlayerControlls>();
    }

    private void Update()
    {
        if (!targetReached)
        {
            newFloor.transform.position = Vector3.Lerp(new Vector3(0f, 2 * GameMetrics.upperFloorY, 0f),
                 new Vector3(0f, GameMetrics.upperFloorY, 0f), (Time.time - startTime) / GameMetrics.dropDuration);
            upperFloor.transform.position = Vector3.Lerp(new Vector3(0f, GameMetrics.upperFloorY, 0f),
                Vector3.zero, (Time.time - startTime) / GameMetrics.dropDuration);
            actualFloor.transform.position = Vector3.Lerp(Vector3.zero,
                new Vector3(0f, -GameMetrics.tileVerticalSize, 0f), (Time.time - startTime) / GameMetrics.dropDuration);
            player.transform.position = new Vector3(actualFloor.GetExitTileNumber() * GameMetrics.tileHorizontalSize
                , player.transform.position.y, 0.0f);
            if (upperFloor.transform.position.y == 0f)
            {
                targetReached = true;
                if (previousFloor != null) Destroy(previousFloor.gameObject);
                previousFloor = actualFloor;
                actualFloor = upperFloor;
                upperFloor = newFloor;
                timeLeft = actualFloor.timeForFloor;
                previousFloor.DeleteTraps();
            }
        }
        else
        {
            timeLeft -= Time.deltaTime;
            //Debug.Log(timeLeft);

            // Initial implementation of drawing text on screen.
            // !!! Don't forget to drag "ui_text_timeLeft" from Hierarchy to 
            // TODO: 
            // Size, color etc. to modify later
            // "Game over" after collision with trap

            int nTimeLeft = (int)timeLeft;
            ui_text_timeLeft.text = "Time Left: " + nTimeLeft.ToString();

            if (timeLeft < 0)
            {
                DropUpperFloor();
                ui_text_timeLeft.text = "Game Over";
            }
        }
    }

    void CreateFirstFloor()
    {
        actualFloor = Instantiate(floorPrefabList[Random.Range(0, floorPrefabList.Length)], Vector3.zero, this.transform.rotation, this.transform);
        actualFloor.entranceTileNumber = -1;
        actualFloor.CreateTiles();
        exitTileNumber = actualFloor.GetExitTileNumber();
        timeLeft = actualFloor.timeForFloor;

        upperFloor = Instantiate(floorPrefabList[Random.Range(0, floorPrefabList.Length)], new Vector3(0f, GameMetrics.upperFloorY, 0f),
            this.transform.rotation, this.transform);
        upperFloor.entranceTileNumber = exitTileNumber;
        upperFloor.CreateTiles();
        exitTileNumber = upperFloor.GetExitTileNumber();
    }

    void CreateNewFloor()
    {
        newFloor = Instantiate(floorPrefabList[Random.Range(0, floorPrefabList.Length)], new Vector3(0f, 2 * GameMetrics.upperFloorY, 0f),
            this.transform.rotation, this.transform);
        newFloor.entranceTileNumber = exitTileNumber;
        newFloor.CreateTiles();
        exitTileNumber = newFloor.GetExitTileNumber();
    }

    public void DropUpperFloor()
    {
        CreateNewFloor();
        startTime = Time.time;
        targetReached = false;
    }
}