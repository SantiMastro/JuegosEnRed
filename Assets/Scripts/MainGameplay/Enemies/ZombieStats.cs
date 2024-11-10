using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    [Header("Zombie Stats")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 10;
    [SerializeField] private int scoreValue = 50;

    [Header("Drop Settings")]
    [SerializeField] private float dropChance = 0.3f; // 30% probabilidad total de dropeo
    [SerializeField] private float healthDropChance = 0.8f; // 80% de ese 30% para vida
    [SerializeField] private GameObject healthDropPrefab;
    [SerializeField] private GameObject ammoDropPrefab;

    // Método para aplicar daño al zombie
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Probabilidad de dropeo
        if (Random.value <= dropChance)
        {
            DropItem();
        }

        PhotonNetwork.Destroy(gameObject); // Destruye el zombie en red
    }

    private void DropItem()
    {
        float dropRoll = Random.value;
        GameObject dropItem;

        if (dropRoll <= healthDropChance) // 80% de probabilidad de dropear vida dentro del 30%
        {
            dropItem = healthDropPrefab;
        }
        else // 20% de probabilidad de dropear munición dentro del 30%
        {
            dropItem = ammoDropPrefab;
        }

        // Instancia el ítem de dropeo en la posición del zombie
        PhotonNetwork.Instantiate(dropItem.name, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        // Movimiento del zombie
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
