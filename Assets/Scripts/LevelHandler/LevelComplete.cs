using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            collision.gameObject.CompareTag("Player") &&
            collision.gameObject.GetComponent<PlayerController>() != null
           )
        {
            LevelManager.Instance.setCurrentLevelDone();

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
    }
}
