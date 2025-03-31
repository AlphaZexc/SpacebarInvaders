using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LetterGameManager : MonoBehaviour
{
    public GameObject hexPrefab;
    public Transform gridParent;
    public int hexCount = 6;
    public Text wordDisplay;
    public HashSet<string> validWords;

    private List<Hexagon> hexTiles = new List<Hexagon>();
    private string currentWord = "";
    private List<Hexagon> selectedTiles = new List<Hexagon>();

    void Start()
    {
        GenerateHexGrid();
    }

    void GenerateHexGrid()
    {
        for (int i = 0; i < hexCount; i++)
        {
            GameObject hex = Instantiate(hexPrefab, gridParent);
            Hexagon hexTile = hex.GetComponent<Hexagon>();
            char randomLetter = (char)('A' + Random.Range(0, 26));
            hexTile.SetLetter(randomLetter);
            hexTiles.Add(hexTile);
        }
    }

    public void OnHexSelected(Hexagon tile)
    {
        if (!selectedTiles.Contains(tile))
        {
            selectedTiles.Add(tile);
            currentWord += tile.GetLetter();
            wordDisplay.text = currentWord;
        }
    }

    public void OnHexDeselected()
    {
        if (validWords.Contains(currentWord))
        {
            Debug.Log("Valid word: " + currentWord);
        }
        ResetSelection();
    }

    void ResetSelection()
    {
        currentWord = "";
        selectedTiles.Clear();
        wordDisplay.text = "";
    }
}
