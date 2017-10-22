using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public int floorSize;
    public int entranceTileNumber; //numer tile który ma być pusty żeby dało się wejść na piętro
    public float timeForFloor;

    public GameObject tilePrefab;
    public GameObject keyPrefab;
    public Trap[] trapsPrefabs;

    private int exitTileNumber;
    private int keyTileNumber;
    private List<Trap> trapList;
    private List<int> tilesList;

    void Awake()
    {
        floorSize = GameMetrics.floorSize;
        trapList = new List<Trap>();
        tilesList = new List<int>();
    }

    public void CreateTiles()
    {
        for (int i = 0; i < floorSize; i++)
        {
            if (i != entranceTileNumber)
            {
                Instantiate(tilePrefab,
                    new Vector3(i * GameMetrics.tileHorizontalSize, this.transform.position.y, 0f),
                    this.transform.rotation, this.transform);
                tilesList.Add(i);
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
        CountTimeForFloor();
    }

    private void CreateExitTile()
    {
        do
        {
            exitTileNumber = Random.Range(0, tilesList.Count);
        }
        while (exitTileNumber == entranceTileNumber || Mathf.Abs(exitTileNumber - entranceTileNumber) < floorSize / 3);
        tilesList.RemoveAt(exitTileNumber);
        Instantiate(transform.parent.GetComponent<LevelCreator>().exitPrefab,
            new Vector3(exitTileNumber * GameMetrics.tileHorizontalSize, this.transform.position.y + GameMetrics.tileHorizontalSize, 0f),
            this.transform.rotation, this.transform);
    }

    private void CreateKey()
    {
        do
        {
            keyTileNumber = Random.Range(0, tilesList.Count);
        }
        while (keyTileNumber == entranceTileNumber || keyTileNumber == exitTileNumber);
        tilesList.RemoveAt(keyTileNumber);
        Instantiate(keyPrefab,
            new Vector3(keyTileNumber * GameMetrics.tileHorizontalSize, this.transform.position.y + GameMetrics.tileHorizontalSize, 0f),
            this.transform.rotation, this.transform);
    }

    private void CreateTraps()
    {
        int trapTileNumber;

        for (int i = 0; i < GameMetrics.numberofTrappedTiles;)
        {
            Trap prefab;
            int tryCounter = 0;
            do
            {
                prefab = trapsPrefabs[Random.Range(0, trapsPrefabs.Length)];
                trapTileNumber = tilesList[Random.Range(0, tilesList.Count)];
                tryCounter++;
            }
            while (trapTileNumber == entranceTileNumber || trapTileNumber == exitTileNumber || trapTileNumber == keyTileNumber
            || CheckTrapPosition(trapTileNumber, prefab.size) || tryCounter < 10);
            Trap trap = Instantiate(prefab,
                new Vector3(trapTileNumber * GameMetrics.tileHorizontalSize, this.transform.position.y + GameMetrics.tileHorizontalSize, 0f),
                prefab.transform.rotation, this.transform);
            for (int a = 0; a < trap.size; a++)
            {
                tilesList.Remove(trapTileNumber + a);
            }
            trap.SetPositionOnFloor(trapTileNumber);
            trapList.Add(trap);
            i += trap.size;
        }
    }

    private bool CheckTrapPosition(int tileNumber, int trapSize)
    {
        int position = tilesList.IndexOf(tileNumber);
        if (tileNumber > (floorSize - trapSize)) return true;
        if (position + trapSize > tilesList.Count) return true;
        if (entranceTileNumber == -1 && tileNumber == 0) return true;


        for (int a = 0; a < trapSize; a++)
        {
            if (tilesList[position + a] != tileNumber + a) return true;
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

    private void CountTimeForFloor()
    {
        float multiplier = 1f;
        int distanceFromEntranceToKey = Mathf.Abs(Mathf.Abs(entranceTileNumber) - keyTileNumber);
        int distanceFromKeyToExit = Mathf.Abs(keyTileNumber - exitTileNumber);
        timeForFloor = (distanceFromEntranceToKey + distanceFromKeyToExit) * multiplier;
    }
}