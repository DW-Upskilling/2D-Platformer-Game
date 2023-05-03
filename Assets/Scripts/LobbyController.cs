using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyController : MonoBehaviour
{
    public GameObject mainMenu, levelSelection;
    public Button startButton, exitButton;
    public Button[] levelButtons;

    void Awake()
    {
        int lobbySceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("lobbySceneIndex", lobbySceneIndex);
        PlayerPrefs.Save();
    }

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(startGame);
    }

    private void startGame()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            string levelName = levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text;
            if (LevelManager.Instance.GetLevelStatus(levelName) == LevelStatus.Locked)
                levelButtons[i].enabled = false;
        }
    }

}
