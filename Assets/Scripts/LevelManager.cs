using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Slider xpSlider;
    public TextMeshProUGUI levelText;
    public GameObject powerupPanel;
    public TextMeshProUGUI choiceText1;
    public TextMeshProUGUI choiceText2;

    public static LevelManager instance { get; private set; }

    private int level;
    private float currentXp;
    private float maxXp = 10;
    private float xpGrowthValue = 1.2f;
    private string choice1, choice2;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);

        UpdateUI();
        powerupPanel.SetActive(false);
    }

    public void AddXP(float amount)
    {
        currentXp += amount;
        if (currentXp >= maxXp)
        {
            LevelUp();
        }
        UpdateUI();
    }

    private void LevelUp()
    {
        currentXp -= maxXp;
        level++;
        maxXp *= xpGrowthValue; // Increase XP requirement for next level

        powerupPanel.SetActive(true);

        GenerateChoices();

        WordManager.instance.EnableHexagons(false);
    }

    private void GenerateChoices()
    {
        int choice1 = Random.Range(1, 4);
        int choice2 = Random.Range(1, 4);

        while (choice1 == choice2)
            choice2 = Random.Range(1, 4);

        switch (choice1) 
        {
            case 1:
                this.choice1 = "Add Letter";
                break;
            case 2:
                this.choice1 = "Get Bomb";
                break;
            case 3:
                this.choice1 = "1.5x Ammo";
                break;
        }

        switch (choice2)
        {
            case 1:
                this.choice2 = "Add Letter";
                break;
            case 2:
                this.choice2 = "Get Bomb";
                break;
            case 3:
                this.choice2 = "1.5x Ammo";
                break;
        }


        choiceText1.text = this.choice1;
        choiceText2.text = this.choice2;
    }

    public void PickChoice(int choice)
    {
        if (choice == 1)
        {
            Debug.Log($"Choice 1: {choice1}");
        }
        else
        {
            Debug.Log($"Choice 2: {choice2}");
        }
    }

    private void UpdateUI()
    {
        levelText.text = level.ToString();
        xpSlider.maxValue = maxXp;
        xpSlider.value = currentXp;
    }
}
