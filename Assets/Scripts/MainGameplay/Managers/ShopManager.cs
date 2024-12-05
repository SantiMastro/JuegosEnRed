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
    public int shotgunPrice = 20;
    public int uziPrice = 20;

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

    [Header("UI - TiendaArmas")]
    public GameObject NotPurchasedShotgun;
    public GameObject NotPurchasedUzi;
    public GameObject PurchasedShotgun;
    public GameObject PurchasedUzi;

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
        if (statsManager.totalCoins >= fullHealthPrice)
        {
            GameObject playerObject = PhotonNetwork.LocalPlayer.TagObject as GameObject;

            if (playerObject != null)
            {
                PlayerController playerController = playerObject.GetComponent<PlayerController>();

                if (playerController != null)
                {
                    statsManager.AddCoinToPool(-fullHealthPrice);
                    playerController.SetHealth();
                    UpdateUI();
                }
            }
        }
    }

    public void BuyShotgun()
    {
        if (statsManager.totalCoins >= shotgunPrice)
        {
            GameObject playerObject = PhotonNetwork.LocalPlayer.TagObject as GameObject;

            if (playerObject != null)
            {
                PlayerController playerController = playerObject.GetComponent<PlayerController>();

                if (playerController != null)
                {
                    statsManager.AddCoinToPool(-shotgunPrice);
                    playerController.shotgunPurchased = true;
                    NotPurchasedShotgun.SetActive(false);
                    PurchasedShotgun.SetActive(true);
                    UpdateUI();
                }
            }
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para comprar la Shotgun.");
        }
    }

    public void BuyUzi()
    {
        if (statsManager.totalCoins >= uziPrice)
        {
            GameObject playerObject = PhotonNetwork.LocalPlayer.TagObject as GameObject;

            if (playerObject != null)
            {
                PlayerController playerController = playerObject.GetComponent<PlayerController>();

                if (playerController != null)
                {
                    statsManager.AddCoinToPool(-uziPrice);
                    playerController.uziPurchased = true;
                    NotPurchasedUzi.SetActive(false);
                    PurchasedUzi.SetActive(true);
                    UpdateUI();
                }
            }
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para comprar la Uzi.");
        }
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
    }
}

