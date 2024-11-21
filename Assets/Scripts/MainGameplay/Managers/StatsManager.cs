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
    [SerializeField] public Image healthBarFill;
    public int currentLifePlayer;

    public static StatsManager instance;

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
        Debug.Log("Total coins is: " + totalCoins);
    }
    public void AddPistolAmmoToPool(int pistolAmmoValue)
    {
        totalPistolAmmo += pistolAmmoValue;
        UpdateText();
        Debug.Log("Total PISTOL_AMMO is: " + totalPistolAmmo);
    }
    public void AddShotgunAmmoToPool(int shotgunAmmoValue)
    {
        totalShotgunAmmo += shotgunAmmoValue;
        UpdateText();
        Debug.Log("Total SHOTGUN_AMMO is: " + totalShotgunAmmo);
    }
    public void AddUziAmmoToPool(int uziAmmoValue)
    {
        totalUziAmmo += uziAmmoValue;
        UpdateText();
        Debug.Log("Total UZI_AMMO is: " + totalUziAmmo);
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
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBarFill.fillAmount = healthPercentage;
    }
}
