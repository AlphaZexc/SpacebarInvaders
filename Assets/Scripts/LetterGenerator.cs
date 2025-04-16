using UnityEngine;
using System.Collections.Generic;

public class LetterGenerator
{
    private char[] consonants = { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z' };
    private char[] vowels = { 'A', 'E', 'I', 'O', 'U' };
    private List<char> usedCharacters = new List<char>();

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

    private char GetRandomCharacter(char[] sourceArray)
    {
        List<char> tempList = new List<char>(sourceArray);
        bool newChar = true;

        int index = Random.Range(0, tempList.Count);

        while (newChar)
        {
            foreach (var letter in usedCharacters)
            {
                if (tempList[index] == letter)
                    index = Random.Range(0, tempList.Count);
                    continue;
            }

            newChar = false;
        }

        usedCharacters.Add(tempList[index]);
        return tempList[index];
    }

    private List<char> GetRandomCharacters(char[] sourceArray, int count)
    {
        List<char> tempList = new List<char>();

        for (int i = 0; i < count; i++)
        {
            tempList.Add(GetRandomCharacter(sourceArray));
        }

        return tempList;
    }
}
