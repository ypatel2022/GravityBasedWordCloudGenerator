using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word
{
    public string word;

    public int amount;

    public Vector2 rectSize;
    public Vector2 position;

    public static bool doneMaking = false;


    public Word(string word, int amount, Vector2 rectSize, Vector2 position)
    {
        this.word = word;
        this.amount = amount;
        this.rectSize = rectSize;
        this.position = position;
    }

    public void Spawn()
    {

    }
}

public static class Words
{
    public static List<Word> word = new List<Word>();
}