using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp { get; private set; }
    public TextMeshProUGUI healthText;

    private float speed = 1f;
    private float fallDistance = 3f;
    private int xpValue;
    private PlayerInfo playerInfo => PlayerInfo.instance;

    public void Initialize(int health)
    {
        hp = health;
        xpValue = health;
        UpdateHealth();
    }

    void Update()
    {
        if (hp <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        LevelManager.instance.AddXP(xpValue);

        EnemySpawner.instance.RemoveDeadEnemies();
    }

    private void UpdateHealth()
    {
        healthText.text = hp.ToString();
    }

    public void TakeDamage()
    {
        hp -= playerInfo.playerDamage;
        UpdateHealth();
    }

    void OnMouseDown()
    {
        if (playerInfo.playerAmmo > 0)
        {
            TakeDamage();
            playerInfo.SetAmmo(playerInfo.playerAmmo - 1);
        }
    }

    public void StartDescending()
    {
        transform.position += Vector3.down * speed * fallDistance;

        // StartCoroutine(Descend());

        // transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, transform.position.x + (speed * fallDistance), 5), transform.position.y, transform.position.z);
    }

    //private IEnumerator Descend()
    //{
    //    float prevX = transform.position.x;
    //    float targetX = prevX + (speed * fallDistance);

    //    while (transform.position.x <= targetX)
    //    {
    //        float currentX = Mathf.MoveTowards(transform.position.x, targetX, 5);

    //        transform.position = new Vector3(currentX, transform.position.y, transform.position.z);

    //        yield return null;
    //    }
    //}
}
