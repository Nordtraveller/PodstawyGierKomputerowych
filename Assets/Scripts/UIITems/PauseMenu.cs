using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {



    public GameObject PauseObject;

    public GameObject HighScorePanel;

    public GameObject GameOptionsPanel;

    public GameObject HUDOptionsPanel;

    private bool IsGameOptions = false;

    private bool IsScore = false;

    private bool IsPaused = false;

    private bool IsHUDOptions = false;

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
            CollapseAll();
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
        CollapseAll();
        IsGameOptions = !IsGameOptions;
        GameOptionsPanel.SetActive(IsGameOptions);
        //SceneManager.LoadScene(1); -> difficulty, graphic, ?skins?
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Score()
    {
        CollapseAll();
        IsScore = !IsScore;
        HighScorePanel.SetActive(IsScore);
    }

    public void HUDOptions()
    {
        CollapseAll();
        IsHUDOptions = !IsHUDOptions;
        HUDOptionsPanel.SetActive(IsHUDOptions);
    }



    private void ScoreOff()
    {
        if (IsScore)
        {
            IsScore = !IsScore;
            HighScorePanel.SetActive(IsScore);
        }
    }
    private void OptionsOff()
    {
        if (IsGameOptions)
        {
            IsGameOptions = !IsGameOptions;
            GameOptionsPanel.SetActive(IsGameOptions);
        }
    }

    private void HUDOptionsOff()
    {
        if (IsHUDOptions)
        {
            IsHUDOptions = !IsHUDOptions;
            HUDOptionsPanel.SetActive(IsHUDOptions);
        }
    }

    private void CollapseAll()
    {
        IsScore = false;
        IsGameOptions = false;
        IsHUDOptions = false;
        HighScorePanel.SetActive(IsScore);
        GameOptionsPanel.SetActive(IsGameOptions);
        HUDOptionsPanel.SetActive(IsHUDOptions);
    }
}
