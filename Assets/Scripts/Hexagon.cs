using TMPro;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    private char letter;
    private TextMeshProUGUI letterText;

    public void Initialize(char letter)
    {
        this.letter = letter;
        letterText = GetComponentInChildren<TextMeshProUGUI>();

        letterText.text = letter.ToString();
    }

    public void SetLetter(char newLetter)
    {
        letter = newLetter;
        
        letterText.text = letter.ToString();
    }

    public char GetLetter()
    {
        return letter;
    }

    private void OnMouseDown()
    {
        WordManager.instance.AddLetter(letter);
    }
}
