using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ZombieStats")]
public class ZombieStatsSO : ScriptableObject
{
    [Header("Zombie Stats")]
    public float speed;
    public int health;
    public int damage;
    public int scoreValue;

    [Header("Drop Settings")]
    public float dropChance; // 30% overall drop chance
    public float healthDropChance; // 80% of dropChance for health
    public GameObject healthDropPrefab;
    public GameObject ammoDropPrefab;
}
