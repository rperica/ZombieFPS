using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class GameScreenManager : MonoBehaviour
{
    #region Singleton
    private static GameScreenManager _instance;

    public static GameScreenManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameScreenManager");
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

    public TMP_Text scoreTextInGame;
    public TMP_Text scoreTextInPause;

    public Slider mouseSensitivitySlider;
    public List<Screen> screens;

    public UnityAction onSettingChange;

    private PauseState pauseState;

    private void UpdateScoreText()
    {
        scoreTextInGame.text = "Score: " + GameManager.Instance.score.ToString();
    }

    public void ToggleScreen(string screenName)
    {
        ScreenID screenID = (ScreenID)Enum.Parse(typeof(ScreenID), screenName);
        screens.Find(x => x.screenID == screenID).ActivateScreen();

        if(!screens.Find(x=>x.screenID==ScreenID.Pause).screen.activeInHierarchy)
        {
            pauseState = PauseState.Other;
        }
        else
        {
            pauseState = PauseState.Pause;
        }
    }

    private void Start()
    {
        pauseState = PauseState.Pause;
        mouseSensitivitySlider.value = GameManager.Instance.mouseSensitivity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseState==PauseState.Pause)
        {
            if (!screens.Find(x=>x.screenID==ScreenID.Pause).screen.activeInHierarchy)
            {
                screens.Find(x => x.screenID == ScreenID.Pause).screen.SetActive(true);
                scoreTextInPause.text = scoreTextInGame.text;
                scoreTextInGame.transform.gameObject.SetActive(false);
                GameManager.Instance.PauseState();
            }
            else
            {
                scoreTextInGame.transform.gameObject.SetActive(true);
                screens.Find(x => x.screenID == ScreenID.Pause).screen.SetActive(false);
                GameManager.Instance.PlayState();
            }
        }

        UpdateScoreText();
    }

    #region ButtonMethods
    #region Pause
    public void Resume()
    {
        scoreTextInGame.transform.gameObject.SetActive(true);
        screens.Find(x => x.screenID == ScreenID.Pause).screen.SetActive(false);
        GameManager.Instance.PlayState();
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.LoadScene(SceneID.MenuScene);
    }
    #endregion
    #endregion

    public void UpdateSettingValue()
    {
        onSettingChange.Invoke();
    }

    private enum PauseState
    {
        Pause,
        Other,
    }
}
