using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuScreenManager : MonoBehaviour
{
    #region Singleton
    private static MenuScreenManager _instance;

    public static MenuScreenManager Instance
    {
        get
        {
            if(_instance==null)
            {
                GameObject go = new GameObject("MenuScreenManager");
                go.AddComponent<MenuScreenManager>();
            }

            return _instance;
        }
    }
    #endregion

    private void Awake()
    {
        #region Singleton
        _instance = this;
        #endregion
    }

    private void Start()
    {
        mouseSensitivitySlider.value = GameManager.Instance.mouseSensitivity;
        ShowScore();
    }

    public List<Screen> screens;
    public TMP_InputField inputField;
    public Slider mouseSensitivitySlider;
    public TMP_Text scoreBoardText;
    
    public string defaultPlayerName = "Player";

    #region ButtonMethods
    public void ToggleScreen(string screenName)
    {
        ScreenID screenID = (ScreenID)Enum.Parse(typeof(ScreenID), screenName);
        screens.Find(x => x.screenID == screenID).ActivateScreen();
    }

    #region Play
    public void StartGame()
    {
        GameManager.Instance.score = 0;
        GameManager.Instance.playerName = (String.IsNullOrEmpty(inputField.text)) ? defaultPlayerName : inputField.text;

        GameManager.Instance.PlayState();
        GameManager.Instance.LoadScene(SceneID.GameScene);
    }
    #endregion

    #region ScoreBoard
    public void ClearScoreBoard()
    {
        SaveManager.Instance.DeleteScoreSave();
        ShowScore();
    }

    public void ShowScore()
    {
        Save scoreSave = SaveManager.Instance.LoadScore();

        if(scoreSave==null)
        {
            Debug.Log("No file ");
            scoreBoardText.text = null;
        }
        else
        {
            scoreBoardText.text = scoreSave.playerName + " " + scoreSave.score;
        }
    }
    #endregion

    #region Settings
    public void OnSliderChanger()
    {
        GameManager.Instance.mouseSensitivity = mouseSensitivitySlider.value;
    }
    #endregion

    #region Quit
    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
    #endregion
    #endregion
}
