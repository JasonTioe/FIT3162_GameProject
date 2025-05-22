using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu; // Assign your Game Over UI Panel here

    private bool isGameOver = false;

    /// <summary>
    /// Call this when the player dies to show the Game Over screen.
    /// </summary>
    public void Show()
    {
        if (isGameOver) return;

        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        isGameOver = true;
    }

    /// <summary>
    /// Reloads the current level. Hook this to your Restart button.
    /// </summary>
    public void Restart()
    {
        // Reset the global game-over flag and unpause
        GameController.gameIsOver = false;
        Time.timeScale = 1f;
        isGameOver = false;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Returns to the Main Menu. Hook this to your Main Menu button.
    /// </summary>
    public void MainMenu()
    {
        // Reset the global game-over flag and unpause
        GameController.gameIsOver = false;
        Time.timeScale = 1f;
        isGameOver = false;

        // Load the Main Menu scene (make sure the name matches your Build Settings)
        SceneManager.LoadScene("Main_Menu");
    }
}
