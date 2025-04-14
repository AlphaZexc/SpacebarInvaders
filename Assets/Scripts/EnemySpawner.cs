using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance { get; private set; }
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    private float enemyHealth = 1;
    private float enemyHealthCurve = 1;

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

        enemyScript.Initialize(Mathf.CeilToInt(enemyHealth));
        activeEnemies.Add(enemyScript);

        enemyHealth = (20 / (1 + Mathf.Pow((float)2.71, (float)(-0.15 * enemyHealthCurve + 2)))) - 2;
        enemyHealthCurve++;
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
}
