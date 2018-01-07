﻿using System.Collections;
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
        HUDElements[0].SetActive(!HUDElements[0].active);
        setColorsHUD();
    }

    public void AbilitiesDisplayChangeStatus()
    {
        HUDElements[1].SetActive(!HUDElements[1].active);
        setColorsHUD();
    }

    public void MiniMapDisplayChangeStatus()
    {
        HUDElements[2].SetActive(!HUDElements[2].active);
        setColorsHUD();
    }

    public void lvlCountDisplayChangeStatus()
    {
        HUDElements[3].SetActive(!HUDElements[3].active);
        setColorsHUD();
    }


    public void SoundsActiveChangeStatus()
    {
        Sounds.mute = !Sounds.mute;
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
}
