using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public int MaxHealt => zombieStats.health;
    public int CurrentHealth => currentHealth;

    [Header("Zombie Settings")]
    public ZombieStatsSO zombieStats;
    [SerializeField] private LayerMask _hitteableLater;

    private Transform closestPlayerTransform;
    private Rigidbody2D rb;
    public int currentHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = MaxHealt;
    }

    private void Update()
    {
        FindClosestPlayer();

        if (closestPlayerTransform != null)
        {
            FollowPlayer();
        }
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
        Vector2 direction = (closestPlayerTransform.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * zombieStats.speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{currentHealth} + {name}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");

        PhotonView enemyPhotonView = GetComponent<PhotonView>();

        if (enemyPhotonView != null && enemyPhotonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
            StatsManager.instance.AddHighScoreToPool(zombieStats.scoreValue);

            if (PhotonNetwork.IsMasterClient)
            {
                if (Random.Range(0f, 1f) <= zombieStats.dropChance)
                {
                    DropItem();
                }
            }
        }
        else
        {
            enemyPhotonView.RPC("DestroyEnemy", RpcTarget.MasterClient);
        }
    }

    private void DropItem()
    {
        float dropRoll = Random.Range(0f, 1f);

        float dropChance = zombieStats.goldCoinDropChance + zombieStats.silverCoinDropChance + zombieStats.bronzeCoinDropChance + zombieStats.pistolAmmoDropChance + zombieStats.uziAmmoDropChance + zombieStats.shotgunAmmoDropChance;

        if (dropRoll <= dropChance)
        {
            float dropTypeRoll = Random.Range(0f, 1f);

            if (dropTypeRoll <= zombieStats.goldCoinDropChance)
            {
                PhotonNetwork.Instantiate(zombieStats.goldCoinPrefab.name, transform.position, Quaternion.identity);
            }
            else if (dropTypeRoll <= zombieStats.goldCoinDropChance + zombieStats.silverCoinDropChance)
            {
                PhotonNetwork.Instantiate(zombieStats.silverCoinPrefab.name, transform.position, Quaternion.identity);
            }
            else if (dropTypeRoll <= zombieStats.goldCoinDropChance + zombieStats.silverCoinDropChance + zombieStats.bronzeCoinDropChance)
            {
                PhotonNetwork.Instantiate(zombieStats.bronzeCoinPrefab.name, transform.position, Quaternion.identity);
            }
            else if (dropTypeRoll <= zombieStats.goldCoinDropChance + zombieStats.silverCoinDropChance + zombieStats.bronzeCoinDropChance + zombieStats.pistolAmmoDropChance)
            {
                PhotonNetwork.Instantiate(zombieStats.pistolAmmoPrefab.name, transform.position, Quaternion.identity);
            }
            else if (dropTypeRoll <= zombieStats.goldCoinDropChance + zombieStats.silverCoinDropChance + zombieStats.bronzeCoinDropChance + zombieStats.pistolAmmoDropChance + zombieStats.uziAmmoDropChance)
            {
                PhotonNetwork.Instantiate(zombieStats.uziAmmoPrefab.name, transform.position, Quaternion.identity);
            }
            else if (dropTypeRoll <= zombieStats.goldCoinDropChance + zombieStats.silverCoinDropChance + zombieStats.bronzeCoinDropChance + zombieStats.pistolAmmoDropChance + zombieStats.uziAmmoDropChance + zombieStats.shotgunAmmoDropChance)
            {
                PhotonNetwork.Instantiate(zombieStats.shotgunAmmoPrefab.name, transform.position, Quaternion.identity);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & _hitteableLater) != 0)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(zombieStats.damage);
            }
        }
    }

    [PunRPC]
    void DestroyEnemy()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}