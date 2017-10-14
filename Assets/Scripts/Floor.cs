using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public int floorSize;
    public int entranceTileNumber; //numer tile który ma być pusty żeby dało się wejść na piętro

    public GameObject tilePrefab;
    public GameObject keyPrefab;
    public GameObject[] trapsPrefabs;

    private int exitTileNumber;
    private int keyTileNumber;

    void Awake()
    {
        floorSize = GameMetrics.floorSize;
    }

    public void CreateTiles()
    {
        for (int i = 0; i < floorSize; i++)
        {
            if(i != entranceTileNumber)
            {
                Instantiate(tilePrefab, 
                    new Vector3(i * GameMetrics.tileHorizontalSize, this.transform.position.y, 0f), 
                    this.transform.rotation, this.transform);
            }
            else
            {
                Instantiate(transform.parent.GetComponent<LevelCreator>().entrancePrefab,
                    new Vector3(i * GameMetrics.tileHorizontalSize, this.transform.position.y, 0f),
                    this.transform.rotation, this.transform);
            }
        }
        CreateExitTile();
        CreateKey();
    }

    private void CreateExitTile()
    {
        do
        {
            exitTileNumber = Random.Range(0, floorSize);
        }
        while (exitTileNumber == entranceTileNumber || Mathf.Abs(exitTileNumber - entranceTileNumber) < floorSize/3 );
        Instantiate(transform.parent.GetComponent<LevelCreator>().exitPrefab,
            new Vector3(exitTileNumber * GameMetrics.tileHorizontalSize, this.transform.position.y + GameMetrics.tileHorizontalSize, 0f), 
            this.transform.rotation, this.transform);
    }

    private void CreateKey()
    {
        do
        {
            keyTileNumber = Random.Range(0, floorSize);
        }
        while (keyTileNumber == entranceTileNumber || keyTileNumber == exitTileNumber);
        Instantiate(keyPrefab, 
            new Vector3(keyTileNumber * GameMetrics.tileHorizontalSize, this.transform.position.y + GameMetrics.tileHorizontalSize, 0f),
            this.transform.rotation, this.transform);
    }

    private void CreateTraps()
    {

    }

    public int GetExitTileNumber()
    {
        return exitTileNumber;
    }
	
}
