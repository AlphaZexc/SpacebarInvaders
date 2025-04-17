using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void PauseGame()
    {
        
    }

    public void RestartGame()
    {
        LevelManager.instance.ResetLevel();
        WordManager.instance.EnableHexagons(true);
        WordManager.instance.ResetWord();
        WordManager.instance.ResetHexagons();
        PlayerInfo.instance.SetDamage(1);

        Time.timeScale = 1f;
        EnemySpawner.instance.ResetEnemies();
    }
}
