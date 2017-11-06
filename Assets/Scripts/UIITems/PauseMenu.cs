using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {



    public GameObject PauseObject;

    private bool IsPaused = false;

    // Use this for initialization
    void Start () {
		PauseObject.SetActive(false);


	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetButtonDown("Pause"))
	    {
	        IsPaused = !IsPaused;

	    }

	    if (IsPaused)
	    {
	        PauseObject.SetActive(true);
	        Time.timeScale = 0;
	    }

	    if (!IsPaused)
	    {
	        PauseObject.SetActive(false);
	        Time.timeScale = 1;
        }
	}

    public void NewGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        IsPaused = false;
    }

    public void Options()
    {
        //SceneManager.LoadScene(1); -> difficulty, graphic, ?skins?
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
