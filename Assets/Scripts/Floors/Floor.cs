using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public int floorSize;
    public int entranceTileNumber; //numer tile który ma być pusty żeby dało się wejść na piętro

    public GameObject tilePrefab;
    public GameObject keyPrefab;
    public Trap[] trapsPrefabs;

    private int exitTileNumber;
    private int keyTileNumber;
    private List<Trap> trapList;

    void Awake()
    {
        floorSize = GameMetrics.floorSize;
        trapList = new List<Trap>();
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
        CreateTraps();
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
        int trapTileNumber;

        for(int i = 0; i < GameMetrics.numberofTrappedTiles;)
        {
            Trap prefab = trapsPrefabs[Random.Range(0, trapsPrefabs.Length)];
            do
            {
                trapTileNumber = Random.Range(0, floorSize);
            }
            while (trapTileNumber == entranceTileNumber || trapTileNumber == exitTileNumber || trapTileNumber == keyTileNumber 
            || CheckTrapPosition(trapTileNumber, prefab.size));
            Trap trap = Instantiate(prefab,
                new Vector3(trapTileNumber * GameMetrics.tileHorizontalSize, this.transform.position.y + GameMetrics.tileHorizontalSize, 0f),
                prefab.transform.rotation, this.transform);
            trap.SetPositionOnFloor(trapTileNumber);
            trapList.Add(trap);
            i += trap.size;
        }
    }

    private bool CheckTrapPosition(int position, int trapSize)
    {
        if (position > (floorSize - trapSize)) return true;
        if (entranceTileNumber == -1 && position == 0) return true;
       
        for(int i = 1; i<trapSize; i++)
        {
        if ((position + i == entranceTileNumber)
         || (position + i == exitTileNumber)
         || (position + i == keyTileNumber))
                return true;
        }

        for (int i = 0; i < trapList.Count; i++)
        {
            for (int j = 0; j < trapList[i].positionOnFloor.Length; j++)
            {
                for (int k = 0; k < trapSize; k++)
                {
                    if ((trapList[i].positionOnFloor[j] == position + k)
                        ) return true;
                }
            }
        }
        return false;
    }

    public void DeleteTraps()
    {
        for (int i = 0; i < trapList.Count; i++)
        {
            Destroy(trapList[i].gameObject);
        }
    }

    public int GetExitTileNumber()
    {
        return exitTileNumber;
    }
	
}


