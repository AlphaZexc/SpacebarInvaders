using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance { get; private set; }

    public int playerDamage { get; private set; } = 1;
    public int playerAmmo { get; private set; } = 0;
    public float ammoMultiplier = 1f;
    public TextMeshProUGUI ammoText;
    public GameObject bombPrefab;
    public Transform itemPanelTransform;

    private Button bombButton;
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

    public void AddBomb()
    {
        bombButton = Instantiate(bombPrefab, itemPanelTransform, false).GetComponent<Button>();

        if (bombButton != null)
        {
            bombButton.onClick.AddListener(UseBomb);
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
        Destroy(bombButton.gameObject);
    }

}
