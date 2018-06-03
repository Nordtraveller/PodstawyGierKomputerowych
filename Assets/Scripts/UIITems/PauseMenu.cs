using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public Image[] HUDActivationDisplayers;

    public Image AudioActivationDisplayer;

    public GameObject[] HUDElements;

    public GameObject PauseObject;

    public GameObject HighScorePanel;

    public GameObject GameOptionsPanel;

    public GameObject HUDOptionsPanel;

    private AudioSource Sounds;

    private bool IsGameOptions = false;

    private bool IsScore = false;

    private bool IsPaused = false;

    private bool IsHUDOptions = false;

    // Use this for initialization
    void Start () {
        Sounds = GameObject.FindGameObjectWithTag("LevelCreator").GetComponent<AudioSource>();
        PauseObject.SetActive(false);
        startUpSetings();
        setColorsHUD();
        setColorsSound();
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


    public void TimerDisplayChangeStatus()
    {
        GameMetrics.isTimer = !GameMetrics.isTimer;
        HUDElements[0].SetActive(GameMetrics.isTimer);
        setColorsHUD();
    }

    public void AbilitiesDisplayChangeStatus()
    {
        GameMetrics.isAbilities = !GameMetrics.isAbilities;
        HUDElements[1].SetActive(GameMetrics.isAbilities);
        setColorsHUD();
    }

    public void MiniMapDisplayChangeStatus()
    {
        GameMetrics.isMiniMap = !GameMetrics.isMiniMap;
        HUDElements[2].SetActive(GameMetrics.isMiniMap);
        setColorsHUD();
    }

    public void lvlCountDisplayChangeStatus()
    {
        GameMetrics.isLvlCount = !GameMetrics.isLvlCount;
        HUDElements[3].SetActive(GameMetrics.isLvlCount);
        setColorsHUD();
    }


    public void SoundsActiveChangeStatus()
    {
        GameMetrics.isMute = !GameMetrics.isMute;
        Sounds.mute = GameMetrics.isMute;
        setColorsSound();
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

    private void setColorsHUD()
    {
        for (int i = 0; i < 4; i++)
        {
            if (HUDElements[i].active)
            {
                HUDActivationDisplayers[i].GetComponent<Image>().color = Color.green;
            }
            else
            {
                HUDActivationDisplayers[i].GetComponent<Image>().color = Color.red;
            }
        }
    }

    private void setColorsSound()
    {
        if (!Sounds.mute)
        {
            AudioActivationDisplayer.GetComponent<Image>().color = Color.green;
        }
        else
        {
            AudioActivationDisplayer.GetComponent<Image>().color = Color.red;
        } 
    }

    private void startUpSetings()
    {
        HUDElements[0].SetActive(GameMetrics.isTimer);
        HUDElements[1].SetActive(GameMetrics.isAbilities);
        HUDElements[2].SetActive(GameMetrics.isMiniMap);
        HUDElements[3].SetActive(GameMetrics.isLvlCount);
        Sounds.mute = GameMetrics.isMute;
    }
}
