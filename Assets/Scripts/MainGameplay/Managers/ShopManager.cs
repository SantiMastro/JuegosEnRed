using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class ShopManager : MonoBehaviour
{
    public StatsManager statsManager;

    [Header("Precios de la tienda")]
    public int pistolAmmoPrice = 5;
    public int shotgunAmmoPrice = 7;
    public int uziAmmoPrice = 15;
    public int fullHealthPrice = 15;

    [Header("UI - Textos")]
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI pistolAmmoText;
    public TextMeshProUGUI shotgunAmmoText;
    public TextMeshProUGUI uziAmmoText;

    [Header("UI - TiendaTextos")]
    public TextMeshProUGUI TcoinsText;
    public TextMeshProUGUI TpistolAmmoText;
    public TextMeshProUGUI TshotgunAmmoText;
    public TextMeshProUGUI TuziAmmoText;

    private PlayerController playerController;

    public void BuyPistolAmmo()
    {
        if (statsManager.totalCoins >= pistolAmmoPrice)
        {
            statsManager.AddCoinToPool(-pistolAmmoPrice);
            statsManager.AddPistolAmmoToPool(5);
            UpdateUI();
        }
    }

    public void BuyShotgunAmmo()
    {
        if (statsManager.totalCoins >= shotgunAmmoPrice)
        {
            statsManager.AddCoinToPool(-shotgunAmmoPrice);
            statsManager.AddShotgunAmmoToPool(5);
            UpdateUI();
        }
    }

    public void BuyUziAmmo()
    {
        if (statsManager.totalCoins >= uziAmmoPrice)
        {
            statsManager.AddCoinToPool(-uziAmmoPrice);
            statsManager.AddUziAmmoToPool(30);
            UpdateUI();
        }
    }

    public void BuyFullHealth()
    {
        //if (statsManager.totalCoins >= fullHealthPrice)
        //{
        //    if (statsManager.totalCoins >= fullHealthPrice)
        //    {
        //        statsManager.AddCoinToPool(-fullHealthPrice);

        //        playerController = PhotonNetwork.LocalPlayer.TagObject as PlayerController;
        //        if (playerController != null)
        //        {
        //            playerController.SetHealth();
        //        }

        //        UpdateUI();
        //    }
        //}
    }

    private void UpdateUI()
    {
        if (coinsText != null)
        {
            coinsText.text = statsManager.totalCoins.ToString();
        }
        if (pistolAmmoText != null)
        {
            pistolAmmoText.text = statsManager.totalPistolAmmo.ToString();
        }
        if (shotgunAmmoText != null)
        {
            shotgunAmmoText.text = statsManager.totalShotgunAmmo.ToString();
        }
        if (uziAmmoText != null)
        {
            uziAmmoText.text = statsManager.totalUziAmmo.ToString();
        }

        if (TcoinsText != null)
        {
            TcoinsText.text = statsManager.totalCoins.ToString();
        }
        if (TpistolAmmoText != null)
        {
            TpistolAmmoText.text = statsManager.totalPistolAmmo.ToString();
        }
        if (TshotgunAmmoText != null)
        {
            TshotgunAmmoText.text = statsManager.totalShotgunAmmo.ToString();
        }
        if (TuziAmmoText != null)
        {
            TuziAmmoText.text = statsManager.totalUziAmmo.ToString();
        }

        //if (statsManager.healthBarFill != null)
        //{
        //    float healthPercentage = (float)statsManager.currentLifePlayer / statsManager.currentLifePlayer;
        //    statsManager.healthBarFill.fillAmount = healthPercentage;
        //}
    }
}

