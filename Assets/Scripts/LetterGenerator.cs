using UnityEngine;
using System.Collections.Generic;

public class LetterGenerator
{

    private List<char> consonants = new List<char>
    {
        'B', 'C', 'C', 'D', 'D', 'D', 'F', 'G', 'G', 'H', 'H', 'J', 'K', 'L', 'L', 'M', 'M',
        'N', 'N', 'P', 'Q', 'R', 'R', 'R', 'S', 'S', 'S', 'T', 'T', 'T', 'T', 'V', 'W', 'X', 'Y', 'Z'
    };

    private List<char> vowels = new List<char>
    {
        'A', 'A', 'A', 'E', 'E', 'E', 'E', 'I', 'I', 'I', 'O', 'O', 'U'
    };

    private List<char> usedCharacters = new List<char>();

    public void ResetCharacters()
    {
        usedCharacters.Clear();
    }

    public List<char> GetRandomLetters(int consonantCount, int vowelCount)
    {
        List<char> selectedLetters = new List<char>();

        selectedLetters.AddRange(GetRandomCharacters(consonants, consonantCount));
        selectedLetters.AddRange(GetRandomCharacters(vowels, vowelCount));

        return selectedLetters;
    }

    public char GetRandomLetter(bool consonant)
    {
        return GetRandomCharacter(consonant ? consonants : vowels);
    }

    private char GetRandomCharacter(List<char> sourceList)
    {
        List<char> tempList = new List<char>(sourceList);
        bool newChar = true;

        int index = Random.Range(0, tempList.Count);

        while (newChar)
        {
            bool matchFound = false;

            foreach (var letter in usedCharacters)
            {
                if (tempList[index] == letter)
                {
                    index = Random.Range(0, tempList.Count);
                    matchFound = true;
                }
            }

            if (!matchFound || usedCharacters.Count >= 8)
                newChar = false;
        }

        usedCharacters.Add(tempList[index]);

        return tempList[index];
    }

    private List<char> GetRandomCharacters(List<char> sourceList, int count)
    {
        List<char> tempList = new List<char>();

        for (int i = 0; i < count; i++)
        {
            tempList.Add(GetRandomCharacter(sourceList));
        }

        return tempList;
    }
}
