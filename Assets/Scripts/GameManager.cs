using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    // used to pass in the input from another script
    [HideInInspector]
    public string input;

    // the word prefab
    public GameObject wordPrefab;

    // the word cloud canvas
    public GameObject wordCloudCanvas;

    // font size multiplier
    public float sizeMultiplier = 3f;

    // the most common words in the english dictionary
    string[] commonWords = {"the", "be", "to", "of", "and", "a", "in", "that", "have", "I", "it", "for", "not", "on", "with", "he", "as", "you", "do", "at", "this", "but", "his", "by", "from", "they", "we", "say", "her", "she", "or", "an", "will", "my", "one", "all", "would", "there", "their", "what", "so", "up", "out", "if", "about", "who", "get", "which", "go", "me", "when", "make", "can", "like", "time", "no", "just",
"him", "know", "take", "people", "into", "year", "your", "good", "some", "could", "them", "see", "other", "than", "then", "now", "look", "only", "come", "its", "over", "think", "also", "back", "after", "use", "two", "how", "our", "work", "first", "well", "way", "even", "new", "want", "because", "any", "these", "give", "day", "most", "us"};

    // list will keep hold all words for later use
    List<CloudWord> cloudWords = new List<CloudWord>();


    public void GenerateCloud(string input)
    {
        // dictionary will hold words and their occurences
        Dictionary<string, int> dictionary = new Dictionary<string, int>();

        // cleaning up input
        input = input.Replace(",", "");
        input = input.Replace(".", "");
        // removes any digits from the string
        // \d identifier matches any digit character
        input = Regex.Replace(input, @"[\d-]", string.Empty);

        // filter out common words
        input = Regex.Replace(input, "\\b" + string.Join("\\b|\\b", commonWords) + "\\b", "");

        //Create an array of words
        string[] arr = input.Split(' ');

        //let's loop over the words
        foreach (string word in arr)
        {
            //if it meets our criteria of at least 3 letters
            if (word.Length > 3)
            {
                //if it's in the dictionary
                if (dictionary.ContainsKey(word))
                {
                    //Increment the count
                    dictionary[word] = dictionary[word] + 1;
                }
                else
                {
                    //put it in the dictionary with a count 1
                    dictionary[word] = 1;
                }
            }
        }

        // store center of screen
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        Vector2 pos = screenCenter;

        //loop through the dictionary
        foreach (KeyValuePair<string, int> pair in dictionary)
        {
            if (pair.Value != 1)
            {
                SpawnWord(pair.Key, pair.Value);
            }
        }
    }

    private void SpawnWord(string word, int amount)
    {
        // random position on the screen
        Vector2 pos = new Vector2(Random.Range(0, 1920 - 100), Random.Range(0, 1080 - 100));

        // instantiate the word prefab
        GameObject wordPrefabClone = Instantiate(wordPrefab, pos, Quaternion.identity) as GameObject;

        cloudWords.Add(wordPrefabClone.GetComponent<CloudWord>());

        // store ref of the gameobject
        TMP_Text wordCloneText = wordPrefabClone.GetComponent<TMP_Text>();

        // change properties of the word
        wordCloneText.transform.SetParent(wordCloudCanvas.transform, false);
        wordCloneText.text = word;
        wordCloneText.fontSize = (int)(amount * sizeMultiplier);
        wordCloneText.color = RandomRGBAValue();
    }

    private Color32 RandomRGBAValue()
    {
        return new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)255);
    }

    public void DeleteWords()
    {
        foreach (CloudWord cloudWord in cloudWords)
        {
            if (cloudWord != null)
            {
                Destroy(cloudWord.gameObject);
            }
        }
    }
}
