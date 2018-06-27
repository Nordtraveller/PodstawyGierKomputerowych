using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private float minX = 9.5f;
    private float maxX = 28.5f;

	
	void LateUpdate ()
    {
        if(player != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = player.transform.position.x;
            if(newPosition.x > minX && newPosition.x < maxX)
            {
                transform.position = newPosition;
            }
            else if(newPosition.x < minX)
            {
                newPosition.x = minX;
                transform.position = newPosition;
            }
            else if (newPosition.x > maxX)
            {
                newPosition.x = maxX;
                transform.position = newPosition;
            }
        }
	}

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
