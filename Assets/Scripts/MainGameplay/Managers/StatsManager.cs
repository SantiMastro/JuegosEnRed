using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class StatsManager : MonoBehaviour
{
    [SerializeField] public int totalCoins = 0;
    [SerializeField] public int totalPistolAmmo = 0;
    [SerializeField] public int totalShotgunAmmo = 0;
    [SerializeField] public int totalUziAmmo = 0;
    [SerializeField] public int totalHighScore = 0;
    [SerializeField] public Image healthBarFill;
    public int currentLifePlayer;

    public static StatsManager instance;

    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI pistolText;
    public TextMeshProUGUI shotgunText;
    public TextMeshProUGUI uziText;

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

    public void AddCoinToPool(int coinsValue)
    {
        totalCoins += coinsValue;
        UpdateText();
    }
    public void AddPistolAmmoToPool(int pistolAmmoValue)
    {
        totalPistolAmmo += pistolAmmoValue;
        UpdateText();
    }
    public void AddShotgunAmmoToPool(int shotgunAmmoValue)
    {
        totalShotgunAmmo += shotgunAmmoValue;
        UpdateText();
    }
    public void AddUziAmmoToPool(int uziAmmoValue)
    {
        totalUziAmmo += uziAmmoValue;
        UpdateText();
    }
    public void AddHighScoreToPool(int highScoreValue)
    {
        totalHighScore += highScoreValue;
        UpdateText();
    }

    public void UpdateText()
    {
        if (coinsText != null)
        {
            coinsText.text = totalCoins.ToString();
        }
        if (pistolText != null)
        {
            pistolText.text = totalPistolAmmo.ToString();
        }
        if (shotgunText != null)
        {
            shotgunText.text = totalShotgunAmmo.ToString();
        }
        if (uziText != null)
        {
            uziText.text = totalUziAmmo.ToString();
        }
        if (highScoreText != null)
        {
            highScoreText.text = totalHighScore.ToString();
        }
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBarFill.fillAmount = healthPercentage;
    }
}
