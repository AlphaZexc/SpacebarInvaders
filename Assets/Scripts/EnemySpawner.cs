using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    private int enemyHealth = 1;

    private List<Enemy> activeEnemies = new List<Enemy>();

    void Start()
    {

    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();

        enemyScript.Initialize(enemyHealth);
        activeEnemies.Add(enemyScript);

        enemyHealth++; // Gradually increase enemy HP
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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

            // Make remaining enemies descend
            foreach (var enemy in activeEnemies)
            {
                enemy.StartDescending();
            }

            SpawnEnemy();
        }
    }
}
