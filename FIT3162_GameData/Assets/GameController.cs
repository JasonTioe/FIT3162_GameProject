using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool gameIsOver = false;
    public BulletSpawner bossSpawner;

    public void TriggerGameOver()
    {
        gameIsOver = true;
        bossSpawner.enabled = false;
    }
}
