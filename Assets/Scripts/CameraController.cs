using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

    public GameObject player;
	
	void LateUpdate ()
    {
        if(player != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = player.transform.position.x;
            transform.position = newPosition;
        }

		if (Input.GetKeyDown (KeyCode.F5)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
}
