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
    public KeyIndicator[] KeyIndicatorPrefab;
    public PowerUp [] powerUpsPrefabs;
    public Trap[] trapsPrefabs;


    public int exitTileNumber;
    public int keyTileNumber;
    public int powerUpTileNumber;
    private List<Trap> trapList;
    private List<PowerUp> powerUpList;
    private List<Key> keyList;
    private List<KeyIndicator> keyIndicatorList;
    private List<int> tilesList;

	private List<GameObject> tilesObjectsList;

    void Awake()
    {
        floorSize = GameMetrics.floorSize;
        trapList = new List<Trap>();
        powerUpList = new List<PowerUp>();
        keyList = new List<Key>();
        keyIndicatorList = new List<KeyIndicator>();
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
        CreateKeyIndicator();
        CreateKey();
        if (Random.Range(0, 100) > 50)
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

    private void CreateKeyIndicator()
    {
        KeyIndicator indicator = Instantiate(KeyIndicatorPrefab[0],
            new Vector3(exitTileNumber * GameMetrics.tileHorizontalSize, this.transform.position.y + GameMetrics.tileHorizontalSize + 1.0f, 0f),
            this.transform.rotation, this.transform);
        keyIndicatorList.Add(indicator);
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

				//Debug.Log(tryCounter);

				if (tryCounter > 20)
				{
					Debug.Log("TU MOGLABY BYĆ ZWIECHA");
					break;
				}
            }
            while (trapTileNumber == entranceTileNumber || trapTileNumber == exitTileNumber || trapTileNumber == keyTileNumber
            || trapTileNumber == powerUpTileNumber || CheckTrapPosition(trapTileNumber, prefab.size));
            Trap trap = Instantiate(prefab,
                new Vector3(trapTileNumber * GameMetrics.tileHorizontalSize, this.transform.position.y + GameMetrics.tileHorizontalSize, 0f),
                prefab.transform.rotation, this.transform);
            trapList.Add(trap);
            for (int a = 0; a < trap.size; a++)
            {
                tilesList.Remove(trapTileNumber + a);
            }
            trap.SetPositionOnFloor(trapTileNumber);
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
        int random = Random.Range(0, powerUpsPrefabs.Length);
        PowerUp powerUp = Instantiate(powerUpsPrefabs[random],
            new Vector3(powerUpTileNumber * GameMetrics.tileHorizontalSize, this.transform.position.y + GameMetrics.tileHorizontalSize, 0f),
            this.transform.rotation, this.transform);
        if (random == 2)
        {
            powerUp.transform.Rotate(transform.rotation.x, transform.rotation.y + 90.0f, transform.rotation.z);
        }
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
        trapList.RemoveAll(x => x == null);
        for (int i = 0; i < trapList.Count; i++)
        {
            GameObject trap = trapList[i].gameObject;
            Destroy(trap);
        }
        trapList.RemoveAll(x => x == null);
    }

    private void DestroyPowerUps()
    {
        powerUpList.RemoveAll(x => x == null);
        for (int i = 0; i < powerUpList.Count; i++)
        {
            Destroy(powerUpList[i].gameObject);
        }
        powerUpList.RemoveAll(x => x == null);
    }

    private void DestroyKey()
    {
        keyList.RemoveAll(x => x == null);
        for (int i = 0; i < keyList.Count; i++)
        {
            Destroy(keyList[i].gameObject);
        }
        keyList.RemoveAll(x => x == null);
    }

    private void DestroyKeyIndicator()
    {
        keyIndicatorList.RemoveAll(x => x == null);
        for (int i = 0; i < keyIndicatorList.Count; i++)
        {
            Destroy(keyIndicatorList[i].gameObject);
        }
        keyIndicatorList.RemoveAll(x => x == null);
    }

    public void DestroyItemsOnFloor()
    {
        DestroyTraps();
        DestroyPowerUps();
        DestroyKey();
        DestroyKeyIndicator();
    }
}