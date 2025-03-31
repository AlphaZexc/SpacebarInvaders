using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Slider xpSlider;
    public TextMeshProUGUI levelText;
    
    public static LevelManager instance { get; private set; }

    private int level;
    private float currentXp;
    private float maxXp = 10;
    private float xpGrowthValue = 1.2f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);

        UpdateUI();
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

    void LevelUp()
    {
        currentXp -= maxXp;
        level++;
        maxXp *= xpGrowthValue; // Increase XP requirement for next level
    }

    void UpdateUI()
    {
        levelText.text = level.ToString();
        xpSlider.maxValue = maxXp;
        xpSlider.value = currentXp;
    }
}
