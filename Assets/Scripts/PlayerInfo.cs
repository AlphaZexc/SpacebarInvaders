using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance { get; private set; }

    public int playerDamage { get; private set; } = 1;
    public int playerAmmo { get; private set; } = 0;
    public float ammoMultiplier = 1f;
    public TextMeshProUGUI ammoText;
    public GameObject bombPrefab;
    public Transform itemPanelTransform;
    public List<Button> bombs;

    private int bombDamage = 20;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(this);
    }

    public void SetAmmo(int ammo)
    {
        playerAmmo = ammo;
        ammoText.text = "Ammo: " + playerAmmo.ToString();
    }

    public void SetDamage(int dmg)
    {
        playerDamage = dmg;
    }

    public void ResetPlayerInfo()
    {
        for (int i = 0; i < bombs.Count; i++)
        {
            Destroy(bombs[i].gameObject);
        }

        bombs.Clear();
        SetDamage(1);
        SetAmmo(0);
    }

    public void AddBomb()
    {
        Button bombButton = Instantiate(bombPrefab, itemPanelTransform, false).GetComponent<Button>();

        if (bombButton != null)
        {
            bombButton.onClick.AddListener(UseBomb);
            bombs.Add(bombButton);
        }
        else
        {
            Debug.LogError("The bombPrefab does not have a Button component!");
        }
    }

    private void UseBomb()
    {
        Debug.Log("Bomb used!");
        EnemySpawner.instance.UseBomb(bombDamage);

        Destroy(bombs[0].gameObject);
        bombs.Remove(bombs[0]);
    }

}
