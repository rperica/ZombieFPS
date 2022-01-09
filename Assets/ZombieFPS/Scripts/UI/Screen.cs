using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    public ScreenID screenID;
    public GameObject screen;
    public GameObject previousScreen;
    public bool isActive;

    public void ActivateScreen()
    {
        if(!isActive)
        {
            isActive = true;
            screen.SetActive(isActive);
            previousScreen.SetActive(!isActive);
        }
        else
        {
            isActive = false;
            screen.SetActive(isActive);
            previousScreen.SetActive(!isActive);
        }
    }
}

public enum ScreenID
{
    Menu,
    Play,
    Settings,
    Credits,
    Pause,
    Scoreboard
}