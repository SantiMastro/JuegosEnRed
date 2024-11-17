using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : MonoBehaviour
{
    [Header("Zombie Settings")]
    public ZombieStatsSO zombieStats;

    private Transform closestPlayerTransform; // Referencia al jugador más cercano
    private Rigidbody2D rb;
    public int currentHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Inicializa la vida del zombie con el valor del ScriptableObject
        currentHealth = zombieStats.health;
    }

    private void Update()
    {
        // Encuentra el jugador más cercano
        FindClosestPlayer();

        // Si hay un jugador cercano, síguelo
        if (closestPlayerTransform != null)
        {
            FollowPlayer();
        }
        Debug.Log(zombieStats.health);
    }

    private void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = Mathf.Infinity;
        closestPlayerTransform = null;

        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                closestPlayerTransform = player.transform;
            }
        }
    }

    private void FollowPlayer()
    {
        // Calcula la dirección hacia el jugador más cercano
        Vector2 direction = (closestPlayerTransform.position - transform.position).normalized;

        // Mueve al zombie hacia el jugador
        rb.MovePosition(rb.position + direction * zombieStats.speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Si la vida llega a 0, el zombie muere
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");

        // Destruye el objeto localmente y en red si estás usando Photon
        Photon.Pun.PhotonNetwork.Destroy(gameObject);
    }
}
