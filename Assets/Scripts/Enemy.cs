using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp { get; private set; }
    public TextMeshProUGUI healthText;

    private float speed = 1f;
    private float fallDistance = 5f;
    private int xpValue;

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
            LevelManager.instance.AddXP(xpValue);
            gameObject.SetActive(false);
        }
    }

    private void UpdateHealth()
    {
        healthText.text = hp.ToString();
    }

    public void TakeDamage()
    {
        hp--;
        UpdateHealth();
    }

    void OnMouseDown()
    {
        TakeDamage();
    }

    public void StartDescending()
    {
        transform.position += Vector3.down * speed * fallDistance;
    }
}
