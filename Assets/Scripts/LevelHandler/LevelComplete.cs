using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public Button backToMainMenuButton;
    public GameObject levelCompleteOverlay;
    // Optional Gameobjects that need to disabled from screen to avoid conflicts
    public GameObject[] toDisable;

    void Awake()
    {
        backToMainMenuButton.onClick.AddListener(backToMainMenu);
    }

    void backToMainMenu()
    {
        int lobbySceneIndex = PlayerPrefs.GetInt("lobbySceneIndex");
        if (lobbySceneIndex >= 0 && lobbySceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(lobbySceneIndex);
        }
        else
        {
            // default to something in build settings
            SceneManager.LoadScene(0);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            collision.gameObject.GetComponent<PlayerController>() != null
           )
        {
            LevelManager.Instance.setCurrentLevelDone();

            levelCompleteOverlay.SetActive(true);

            for (int i = 0; i < toDisable.Length; i++)
                toDisable[i].SetActive(false);
        }
    }
}
