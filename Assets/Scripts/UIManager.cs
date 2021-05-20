using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public TMP_InputField input;

    public GameManager gameManager;

    public void Generate()
    {
        gameManager.GenerateCloud(input.text);
    }
}
