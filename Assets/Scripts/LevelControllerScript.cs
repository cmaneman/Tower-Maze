using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerControllerScript : MonoBehaviour
{
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
