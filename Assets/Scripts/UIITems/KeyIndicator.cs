using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyIndicator : MonoBehaviour {

    public GameObject Empty;

    public GameObject Full;

    private GameObject keyEmptyInstance;

    private GameObject keyFullInstance;

    private PlayerStatus PlayerStatus;

    private LevelCreator level;


    // Use this for initialization
    void Start () {
        this.transform.Rotate(new Vector3(180.0f, 0.0f, 0.0f));

        PlayerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        level = GameObject.FindWithTag("LevelCreator").GetComponent<LevelCreator>();

        keyEmptyInstance = Instantiate(Empty, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation) as GameObject;
    }
	
	// Update is called once per frame
	void Update () {

        DeleteKeyIndicatorFromPreviousFloor();
        UpdateKeyStatus();

    }

    private void DeleteKeyIndicatorFromPreviousFloor()
    {
        if(keyEmptyInstance != null)
        {
            if(keyEmptyInstance.transform.position.y <= 2.9)
            {
                Destroy(gameObject);
            }
        }

        if(keyFullInstance != null)
        {
            if(keyFullInstance.transform.position.y <= 2.9)
            {
                Destroy(gameObject);
            }
        }
    }

    private void UpdateKeyStatus()
    {
        if (PlayerStatus.haveKey && keyFullInstance == null)
        {
            Destroy(keyEmptyInstance);
            keyFullInstance = Instantiate(Full, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation) as GameObject;
        }
        else if (!PlayerStatus.haveKey && keyFullInstance != null)
        {
            Destroy(keyFullInstance);

        }
        else if (level.getUpperFloor().transform.position.y <= 0.5 && keyEmptyInstance == null)
        {
            keyEmptyInstance = Instantiate(Empty, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation) as GameObject;
        }
    }

    private void OnDestroy()
    {
        if(keyFullInstance != null)
        {
            Destroy(keyFullInstance);
        }

        if(keyEmptyInstance != null)
        {
            Destroy(keyEmptyInstance);
        }
    }
}
