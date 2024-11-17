using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ZombieStats")]
public class ZombieStatsSO : ScriptableObject
{
    [Header("Zombie Stats")]
    public float speed = 2f;
    public int health = 100;
    public int damage = 10;
    public int scoreValue = 50;

    [Header("Drop Settings")]
    public float dropChance = 0.3f; // 30% overall drop chance
    public float healthDropChance = 0.8f; // 80% of dropChance for health
    public GameObject healthDropPrefab;
    public GameObject ammoDropPrefab;
}
