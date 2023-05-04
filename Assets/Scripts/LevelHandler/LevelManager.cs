using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }

    public string[] levels;

    void Awake()
    {
        if (instance == null)
        {
            // Clear all PlayerPrefs data
            // Only for testing if levels unlock as expected
            // Will be removed if game is published
            PlayerPrefs.DeleteAll();
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (levels.Length > 0)
            SetLevelStatus(levels[0], LevelStatus.Unlocked);
    }

    public LevelStatus GetLevelStatus(string level)
    {
        string key = "levelStatus[" + level + "]";
        Debug.Log("GetLevelStatus:: " + key + ": " + PlayerPrefs.GetInt(key, 0));
        return (LevelStatus)PlayerPrefs.GetInt(key, 0);
    }

    public void setCurrentLevelDone()
    {
        string currentlevelName = SceneManager.GetActiveScene().name;
        SetLevelStatus(currentlevelName, LevelStatus.Done);

        int nextLevelIndex = Array.FindIndex(levels, e => e == currentlevelName) + 1;
        if (nextLevelIndex < levels.Length)
        {
            string nextLevelName = levels[nextLevelIndex];
            SetLevelStatus(nextLevelName, LevelStatus.Unlocked);
        }
    }

    public void SetLevelStatus(string level, LevelStatus levelStatus)
    {
        string key = "levelStatus[" + level + "]";
        PlayerPrefs.SetInt(key, (int)levelStatus);
        Debug.Log(key + ": " + (int)levelStatus);
    }

}
