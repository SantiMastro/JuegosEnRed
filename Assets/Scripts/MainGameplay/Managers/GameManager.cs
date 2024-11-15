using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int totalCoins = 0;
    public TextMeshProUGUI coinsText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoinToPool()
    {
        totalCoins++;
        UpdateCoinsText();
        Debug.Log("Total coins is: " + totalCoins);
    }

    public void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = totalCoins.ToString();
        }
    }
}
