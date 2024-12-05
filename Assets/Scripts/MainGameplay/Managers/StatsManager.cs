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

    [Header("UI - Textos")]
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI pistolText;
    public TextMeshProUGUI shotgunText;
    public TextMeshProUGUI uziText;

    [Header("UI - TiendaTextos")]
    public TextMeshProUGUI TcoinsText;
    public TextMeshProUGUI TpistolText;
    public TextMeshProUGUI TshotgunText;
    public TextMeshProUGUI TuziText;

    [Header("UI - FinalScore")]
    public TextMeshProUGUI FinalScore;

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
        if (totalCoins + coinsValue >= 0)
        {
            totalCoins += coinsValue;
            UpdateText();
        }
        else
        {
            totalCoins = 0;
            UpdateText();
        }
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

        if (TcoinsText != null)
        {
            TcoinsText.text = totalCoins.ToString();
        }
        if (TpistolText != null)
        {
            TpistolText.text = totalPistolAmmo.ToString();
        }
        if (TshotgunText != null)
        {
            TshotgunText.text = totalShotgunAmmo.ToString();
        }
        if (TuziText != null)
        {
            TuziText.text = totalUziAmmo.ToString();
        }
    }

    public void UpdateFinalHighscore()
    {
        if (FinalScore != null)
        {
            FinalScore.text = totalHighScore.ToString();
        }
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBarFill.fillAmount = healthPercentage;
    }
}
