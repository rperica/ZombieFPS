using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
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

        score = 0;
        mouseSensitivity = 2.5f;

        gameState = GameState.Default;
        currentScene = SceneID.MenuScene;
    }

    [HideInInspector] public int score;
    [HideInInspector] public string playerName;


    #region Settings
    [HideInInspector] public float mouseSensitivity;
    #endregion

    private SceneID currentScene;
    private GameState gameState;

    public GameState GameState { get { return gameState; } }

    public void LoadScene(SceneID scene)
    {
        currentScene = scene;
        SceneManager.LoadScene((int)scene);
    }

    public void PlayState()
    {
        Time.timeScale = 1.0f;
        gameState = GameState.Play;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseState()
    {
        Time.timeScale = 0.0f;
        gameState = GameState.Pause;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DefaultState()
    {
        Time.timeScale = 1.0f;
        gameState = GameState.Default;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

public enum SceneID
{
    MenuScene = 0,
    GameScene = 1,
    GameOverScene = 2
}

public enum GameState
{
    Default,
    Play,
    Pause
}


