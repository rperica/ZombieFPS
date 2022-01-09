using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreenManager : MonoBehaviour
{
    #region Singleton
    private static GameOverScreenManager _instance;

    public static GameOverScreenManager Instance
    {
        get
        {
            if(_instance==null)
            {
                GameObject go = new GameObject("GameOverScreenManager");
                go.AddComponent<GameOverScreenManager>();
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

    public TMP_Text scoreText;

    private void Start()
    {
        SaveManager.Instance.SaveScore();

        scoreText.text = GameManager.Instance.playerName.ToString() + " " + GameManager.Instance.score.ToString();
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.LoadScene(SceneID.MenuScene);
    }
}
