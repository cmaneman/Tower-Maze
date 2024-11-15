using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleteUI : MonoBehaviour
{
    public GameObject levelCompleteScreen; // Assign the Canvas GameObject in the Inspector

    void Start()
    {
        // Hide the screen at the start of the game
        levelCompleteScreen.SetActive(false);
    }

    public void ShowLevelCompleteScreen()
    {
        levelCompleteScreen.SetActive(true); // Show the screen when level is complete
    }

    public void NextLevel()
    {
        // Load the next level (assuming levels are sequentially numbered)
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void RestartLevel()
    {
        // Reload the current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        // Load the main menu scene (assuming it’s at index 0 or named "MainMenu")
        SceneManager.LoadScene("MainMenu");
    }
}

