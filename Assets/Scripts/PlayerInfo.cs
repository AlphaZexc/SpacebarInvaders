using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance { get; private set; }

    public int playerDamage { get; private set; } = 1;
    public int playerAmmo { get; private set; } = 0;
    public float ammoMultiplier = 1f;
    public TextMeshProUGUI ammoText;
    public List<string> inventory;

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
}
