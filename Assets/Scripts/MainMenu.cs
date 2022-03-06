using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject creditsOverlay;
    [SerializeField] private GameObject optionsOverlay;
    [SerializeField] private GameObject soundOnButton;
    [SerializeField] private GameObject soundOffButton;
    [SerializeField] private GameObject godModeOnButton;
    [SerializeField] private GameObject godModeOffButton;
    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        creditsOverlay.SetActive(false);
        optionsOverlay.SetActive(false);
        godModeOnButton.SetActive(false);
    }

    public void StartGame()
    {
        levelManager.LoadGame();
    }

    public void HideCredits()
    {
        creditsOverlay.SetActive(false);
    }
    
    public void DisplayCredits()
    {
        creditsOverlay.SetActive(true);
    }

    public void HideOptions()
    {
        optionsOverlay.SetActive(false);
    }

    public void DisplayOptions()
    {
        optionsOverlay.SetActive(true);
    }
    
    public void ToggleSoundButton()
    {
        if (soundOnButton.activeInHierarchy)
        {
            soundOffButton.SetActive(true);
            soundOnButton.SetActive(false);
        }
        else
        {
            soundOffButton.SetActive(false);
            soundOnButton.SetActive(true);
        }
    }
    
    public void ToggleGodModeButton()
    {
        if (godModeOnButton.activeInHierarchy)
        {
            godModeOffButton.SetActive(true);
            godModeOnButton.SetActive(false);
        }
        else
        {
            godModeOffButton.SetActive(false);
            godModeOnButton.SetActive(true);
        }
    }
}
