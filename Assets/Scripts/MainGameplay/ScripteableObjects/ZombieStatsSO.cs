using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZombieStats", menuName = "Stats/ZombieStats", order = 1)]
public class ZombieStatsSO : ScriptableObject
{
    [Header("Zombie Stats")]
    public float speed;
    public int health;
    public int damage;
    public int scoreValue;

    [Header("Drop Settings")]
    public float dropChance;

    [Header("Coin and Ammo Drop Settings")]
    public float goldCoinDropChance;
    public float silverCoinDropChance;
    public float bronzeCoinDropChance;

    public float pistolAmmoDropChance;
    public float uziAmmoDropChance;
    public float shotgunAmmoDropChance;

    [Header("Drop Prefabs")]
    public GameObject goldCoinPrefab;
    public GameObject silverCoinPrefab;
    public GameObject bronzeCoinPrefab;
    public GameObject pistolAmmoPrefab;
    public GameObject uziAmmoPrefab;
    public GameObject shotgunAmmoPrefab;
}
