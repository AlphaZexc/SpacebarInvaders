using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static WordManager instance { get; private set; }
    public GameObject hexagonPrefab;
    public Transform hexagonRoot;
    public TextMeshProUGUI wordText;

    private LetterGenerator letterGenerator = new LetterGenerator();
    private List<char> currentLetters = new List<char>();
    private List<Hexagon> currentHexes = new List<Hexagon>();
    private string currentWord = "";
    private int gridWidth = 3; // Adjust as needed
    private float hexWidth = 1.1f; // Adjust based on hex size
    private float hexHeight = 1.1f; // Adjust based on hex size

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        currentLetters = letterGenerator.GetRandomLetters(3, 2); // 3 consonants, 2 vowels

        GenerateGrid();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetWord();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SubmitWord();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AddHexagon();
        }
    }

    public void GenerateGrid()
    {
        // Reset Grid
        foreach (var hexagon in currentHexes)
        {
            Destroy(hexagon.gameObject);
        }
        currentHexes.Clear();

        // Generate Grid
        for (int i = 0; i < currentLetters.Count; i++)
        {
            int row = i / gridWidth;
            int col = i % gridWidth;

            // Calculate position with offset for staggered rows
            float xOffset = (col * hexWidth) + ((row % 2) * (hexWidth / 2));
            float yOffset = row * (hexHeight * 0.85f); // Slight overlap for correct hex spacing

            Vector3 spawnPos = new Vector3(hexagonRoot.position.x + xOffset, hexagonRoot.position.y - yOffset, hexagonRoot.position.z);

            Hexagon hexagon = Instantiate(hexagonPrefab, spawnPos, Quaternion.identity).GetComponent<Hexagon>();
            hexagon.Initialize(currentLetters[i]);

            currentHexes.Add(hexagon);
        }
    }

    public void AddHexagon()
    {
        int consonant = Random.Range(0, 10);

        if (consonant <= 6)
            currentLetters.Add(letterGenerator.GetRandomLetter(true));
        else
            currentLetters.Add(letterGenerator.GetRandomLetter(false));

        GenerateGrid();
    }

    public void ResetHexagons()
    {
        letterGenerator.ResetCharacters();
        currentLetters = letterGenerator.GetRandomLetters(3, 2); // 3 consonants, 2 vowels
        GenerateGrid();
    }

    public void AddLetter(char letter)
    {
        currentWord += letter;
        wordText.text = currentWord;
    }

    public string GetCurrentWord()
    {
        return currentWord;
    }

    public void SubmitWord()
    {
        if (currentWord != string.Empty)
        {
            StartCoroutine(EnemySpawner.instance.DescendAll());

            PlayerInfo.instance.SetAmmo(PlayerInfo.instance.playerAmmo + Mathf.RoundToInt(currentWord.Length * PlayerInfo.instance.ammoMultiplier));

            ResetWord();
        }
        else
        {
            Debug.Log("Word is empty.");
        }
    }

    public void ResetWord()
    {
        currentWord = string.Empty;
        wordText.text = string.Empty;
    }

    public void EnableHexagons(bool active)
    {
        foreach (var hex in currentHexes)
        {
            hex.gameObject.SetActive(active);
        }
    }
}
