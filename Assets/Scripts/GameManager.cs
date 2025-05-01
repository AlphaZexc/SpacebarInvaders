using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void PauseGame()
    {
        
    }

    public void RestartGame()
    {
        LevelManager.instance.ResetLevel();
        WordManager.instance.ResetWordInfo();

        Time.timeScale = 1f;
        EnemySpawner.instance.ResetEnemies();
    }
}
