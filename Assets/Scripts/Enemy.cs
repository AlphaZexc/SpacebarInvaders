using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp { get; private set; }
    public TextMeshProUGUI healthText;
    public Coroutine isDescending;

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

    public Coroutine StartDescending()
    {
        return StartCoroutine(Descend());
    }

    private IEnumerator Descend()
    {
        float prevY = transform.position.y;
        float targetY = prevY - (speed * fallDistance); // subtract to move downward

        float duration = 0.3f; // total time to complete the movement
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; // 0 to 1
            float newY = Mathf.Lerp(prevY, targetY, t);

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Make sure it ends exactly at the target
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        isDescending = null;
    }

}
