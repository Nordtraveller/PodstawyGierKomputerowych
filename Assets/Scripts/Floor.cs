﻿using System.Collections;
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

        for(int i = 0; i < GameMetrics.numberOfTraps; i++)
        {
            do
            {
                trapTileNumber = Random.Range(0, floorSize);
            }
            while (trapTileNumber == entranceTileNumber || trapTileNumber == exitTileNumber || trapTileNumber == keyTileNumber 
            || CheckTrapPosition(trapTileNumber));
            Trap prefab = trapsPrefabs[Random.Range(0, trapsPrefabs.Length)];
            Trap trap = Instantiate(prefab,
                new Vector3(trapTileNumber * GameMetrics.tileHorizontalSize, this.transform.position.y + GameMetrics.tileHorizontalSize, 0f),
                prefab.transform.rotation, this.transform);
            trapList.Add(trap);
        }
    }

    private bool CheckTrapPosition(int position)
    {
        bool before = false;
        bool beforebefore = false;
        bool after = false;
        bool afterafter = false;
        for (int i = 0; i < trapList.Count; i++)
        {
            if (trapList[i].positionOnFloor == position) return true;
            if (trapList[i].positionOnFloor == position - 2) beforebefore = true;
            if (trapList[i].positionOnFloor == position - 1) before = true;
            if (trapList[i].positionOnFloor == position + 1) after = true;
            if (trapList[i].positionOnFloor == position + 2) afterafter = true;
        }
        if ((before && after) || (beforebefore && before) || (after && afterafter)) return true;
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


