using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    public GameObject mainMenu, levelSelection;
    public Button startButton, exitButton;
    public Button[] levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(startGame);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;
            levelButtons[i].onClick.AddListener(
                () => { LoadLevel(levelIndex); }
            );
        }
    }

    private void startGame()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }

    private void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }

}
