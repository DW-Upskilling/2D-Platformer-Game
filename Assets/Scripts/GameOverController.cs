using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public Button restartButton, exitButton;
    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(restartGame);
        exitButton.onClick.AddListener(exitToMainMenu);
    }

    void restartGame()
    {
        SceneManager.LoadScene(1);
    }

    void exitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
