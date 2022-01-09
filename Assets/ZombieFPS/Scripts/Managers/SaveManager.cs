using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region Singleton
    private static SaveManager _instance;

    public static SaveManager Instance
    {
        get
        {
            if(_instance==null)
            {
                GameObject go = new GameObject("SaveManager");
                go.AddComponent<SaveManager>();
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
    }

    public void SaveScore()
    {
        Save scoreSave = CreateScoreSave();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/score.save"); 
        bf.Serialize(file, scoreSave);
        file.Close();
    }

    public Save LoadScore()
    {
        Save scoreSave;

        if (!File.Exists(Application.persistentDataPath + "/score.save"))
            return null;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenRead(Application.persistentDataPath + "/score.save");
        scoreSave = (Save)bf.Deserialize(file);
        file.Close();

        return scoreSave;
    }
    
    public void DeleteScoreSave()
    {
        if(File.Exists(Application.persistentDataPath + "/score.save"))
        {
            File.Delete(Application.persistentDataPath + "/score.save");
        }
    }

    private Save CreateScoreSave()
    {
        Save save = new Save();

        save.score = GameManager.Instance.score;
        save.playerName = GameManager.Instance.playerName;

        return save;
    }
}

[System.Serializable]
public class Save
{
    public int score;
    public string playerName;
}