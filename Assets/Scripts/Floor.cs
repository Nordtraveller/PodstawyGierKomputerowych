using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public int floorSize;
    public int entranceTileNumber; //numer tile który ma być pusty żeby dało się wejść na piętro

    public GameObject tilePrefab;

    private int exitTileNumber;

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
                Instantiate(tilePrefab, new Vector3(i * GameMetrics.tileSize, this.transform.position.y, 0f), this.transform.rotation, this.transform);
            }
        }
        CreateExitTile();
    }

    private void CreateExitTile()
    {
        do
        {
            exitTileNumber = Random.Range(0, floorSize);
        }
        while (exitTileNumber == entranceTileNumber || Mathf.Abs(exitTileNumber - entranceTileNumber) < floorSize/3 );
        Instantiate(transform.parent.GetComponent<LevelCreator>().exitPrefab,
            new Vector3(exitTileNumber * GameMetrics.tileSize, this.transform.position.y + GameMetrics.tileSize, 0f), this.transform.rotation, this.transform);
        Debug.Log(exitTileNumber);
    }

    public int GetExitTileNumber()
    {
        return exitTileNumber;
    }
	
}
