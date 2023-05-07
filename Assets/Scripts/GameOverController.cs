using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public Button restartButton, exitButton;

    private int previousSceneIndex;

    // Start is called before the first frame update
    void Start()
    {

        AudioManager.Instance.Play("backgroundMusic", false);
        AudioManager.Instance.Play("deathBackgroundMusic");

        // Load the previous scene index from player preferences
        previousSceneIndex = PlayerPrefs.GetInt("previousSceneIndex", -1);

        restartButton.onClick.AddListener(restartGame);
        exitButton.onClick.AddListener(exitToMainMenu);
    }

    void restartGame()
    {
        // SceneManager.LoadScene(1);
        // Load the previous scene if it exists
        if (previousSceneIndex >= 0 && previousSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(previousSceneIndex);
            AudioManager.Instance.Play("backgroundMusic", true);
            AudioManager.Instance.Play("deathBackgroundMusic", false);
        }
    }

    void exitToMainMenu()
    {

        AudioManager.Instance.Play("backgroundMusic", true);
        AudioManager.Instance.Play("deathBackgroundMusic", false);

        SceneManager.LoadScene(0);

    }
}
