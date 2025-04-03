using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance { get; private set; }
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    private int enemyHealth = 1;

    private List<Enemy> activeEnemies = new List<Enemy>();

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();

        enemyScript.Initialize(enemyHealth);
        activeEnemies.Add(enemyScript);

        enemyHealth++; // Gradually increase enemy HP
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

    public void DescendAll()
    {
        foreach (var enemy in activeEnemies)
        {
            enemy.StartDescending();
        }

        SpawnEnemy();
    }
}
