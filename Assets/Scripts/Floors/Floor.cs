using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public int floorSize;
    public int entranceTileNumber; //numer tile który ma być pusty żeby dało się wejść na piętro
    public float timeForFloor;

	public List<GameObject> GetTilesObjectList()
	{
		return tilesObjectsList;
	}

    public GameObject tilePrefab;
    public Key [] keyPrefab;
    public PowerUp [] powerUpsPrefabs;
    public Trap[] trapsPrefabs;


    public int exitTileNumber;
    public int keyTileNumber;
    public int powerUpTileNumber;
    private List<Trap> trapList;
    private List<PowerUp> powerUpList;
    private List<Key> keyList;
    private List<int> tilesList;

	private List<GameObject> tilesObjectsList;

    void Awake()
    {
        floorSize = GameMetrics.floorSize;
        trapList = new List<Trap>();
        powerUpList = new List<PowerUp>();
        keyList = new List<Key>();
        tilesList = new List<int>();
		tilesObjectsList = new List<GameObject> ();
    }

    public void CreateTiles()
    {
        for (int i = 0; i < floorSize; i++)
        {
            if (i != entranceTileNumber)
            {
                GameObject go = Instantiate(tilePrefab,
                    new Vector3(i * GameMetrics.tileHorizontalSize, this.transform.position.y, 0f),
                    this.transform.rotation, this.transform);

				tilesObjectsList.Add (go);

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
        if (Random.Range(0, 100) > 90)
        {
            CreatePowerUp();
        }
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
        Key key = Instantiate(keyPrefab[0],
            new Vector3(keyTileNumber * GameMetrics.tileHorizontalSize, this.transform.position.y + GameMetrics.tileHorizontalSize, 0f),
            this.transform.rotation, this.transform);
        keyList.Add(key);
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
            || trapTileNumber == powerUpTileNumber || CheckTrapPosition(trapTileNumber, prefab.size) || tryCounter < 10);
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

    private void CreatePowerUp()
    {
        do
        {
            powerUpTileNumber = Random.Range(0, tilesList.Count);
        }
        while (powerUpTileNumber == entranceTileNumber || powerUpTileNumber == exitTileNumber || powerUpTileNumber == keyTileNumber);
        tilesList.RemoveAt(powerUpTileNumber);
        PowerUp powerUp = Instantiate(powerUpsPrefabs[Random.Range(0, powerUpsPrefabs.Length)],
            new Vector3(powerUpTileNumber * GameMetrics.tileHorizontalSize, this.transform.position.y + GameMetrics.tileHorizontalSize, 0f),
            this.transform.rotation, this.transform);
        powerUpList.Add(powerUp);

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

    private void DestroyTraps()
    {
        for (int i = 0; i < trapList.Count; i++)
        {
            Destroy(trapList[i].gameObject);
        }
    }


    public void DestroyItemsOnFloor()
    {
        DestroyTraps();
        Destroy(keyList[0].gameObject);
        if(powerUpList.Count != 0)
        {
            Destroy(powerUpList[0].gameObject);
        }
    }
}