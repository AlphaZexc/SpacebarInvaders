using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance { get; private set; }
    public GameObject enemyPrefab;
    public GameObject gameOverPanel;
    public Transform spawnPoint;

    private float enemyHealth = 1;
    private float enemyHealthCurve = 1;
    private List<Enemy> activeEnemies = new List<Enemy>();

    public Button button;

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        gameOverPanel.SetActive(false);
        SpawnEnemy();
    }

    private void Update()
    {
        foreach (var enemy in activeEnemies)
        {
            if (enemy.transform.position.y < -5)
            {
                GameOver();
                break;
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();

        enemyScript.Initialize(Mathf.CeilToInt(enemyHealth));
        activeEnemies.Add(enemyScript);

        enemyHealth = (20 / (1 + Mathf.Pow((float)2.71, (float)(-0.15 * enemyHealthCurve + 2)))) - 2;
        enemyHealthCurve++;
    }

    public void UseBomb(int bombDamage)
    {
        for (int i = 0; i < bombDamage; i++)
        {
            int randomEnemy = Random.Range(0, activeEnemies.Count);

            activeEnemies[randomEnemy].TakeDamage();
        }
    }

    public void RemoveDeadEnemies()
    {
        // Remove destroyed enemies safely
        activeEnemies.RemoveAll(enemy =>
        {
            if (enemy.hp <= 0)
            {
                Destroy(enemy.gameObject);
                return true;
            }
            return false;
        });
    }

    public IEnumerator DescendAll()
    {
        foreach (var enemy in activeEnemies)
        {
            yield return enemy.StartDescending();
        }

        SpawnEnemy();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        enemyHealthCurve = 1;
        enemyHealth = 1;
        PlayerInfo.instance.ResetPlayerInfo();
        WordManager.instance.EnableHexagons(false);
    }

    public void ResetEnemies()
    {
        foreach (var enemy in activeEnemies)
        {
            Destroy(enemy.gameObject);
        }
        activeEnemies.Clear();
        gameOverPanel.SetActive(false);

        SpawnEnemy();
    }
}
