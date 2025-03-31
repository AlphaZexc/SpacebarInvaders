using UnityEngine;
using System.Collections.Generic;

public class LetterGenerator
{
    private char[] consonants = { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z' };
    private char[] vowels = { 'A', 'E', 'I', 'O', 'U' };

    public List<char> GetRandomLetters(int consonantCount, int vowelCount)
    {
        List<char> selectedLetters = new List<char>();

        selectedLetters.AddRange(GetRandomCharacters(consonants, consonantCount));
        selectedLetters.AddRange(GetRandomCharacters(vowels, vowelCount));

        return selectedLetters;
    }

    private List<char> GetRandomCharacters(char[] sourceArray, int count)
    {
        List<char> selected = new List<char>();
        List<char> tempList = new List<char>(sourceArray);

        for (int i = 0; i < count; i++)
        {
            if (tempList.Count == 0) break;

            int index = Random.Range(0, tempList.Count);
            selected.Add(tempList[index]);
            tempList.RemoveAt(index); // Avoid duplicate picks
        }

        return selected;
    }
}
