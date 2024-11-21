using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawn : MonoBehaviour
{
    
    [SerializeField] private GameObject normalZombiePrefab;
    [SerializeField] private GameObject fastZombiePrefab;
    [SerializeField] private GameObject tankZombiePrefab;
    [SerializeField] private GameObject textGameStart;
    [SerializeField] private GameObject waitingPlayers;
    [SerializeField] private Transform[] spawnPoints; // Array de spawn points
    

    [SerializeField] private float timeToStartSpawning = 5f;
    [SerializeField] private float timeBetweenSpawn = 1f;
    [SerializeField] private float timeBetweenWaves = 20f;


    private int waveNumber = 1;
    private int totalZombiesToSpawn = 10; // Zombies en la primera oleada
    private int zombiesSpawned = 0;
    private int normalZombies, fastZombies, tankZombies;
    private bool waveActive = false;
    private float timer;
    private bool matchStarted = false;
    private bool countIsComplete = false;
    
    private void Start()
    {
        timer = 0;
        countIsComplete = false;
        waitingPlayers.SetActive(true);
        textGameStart.SetActive(false);
    }

    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 1)
        {
            matchStarted = true;
            if (countIsComplete == false)
            {
                waitingPlayers.SetActive(false);
                textGameStart.SetActive(true);
            }

            timer += Time.deltaTime;

            if (timer >= timeToStartSpawning && countIsComplete == false)
            {
                textGameStart.SetActive(false);
                countIsComplete = true;
            }

            if (PhotonNetwork.IsMasterClient)
            {
                // Comienza una nueva oleada después del tiempo de espera
                if (!waveActive && timer >= timeToStartSpawning)
                {
                    StartNewWave();
                }

                // Spawnea zombies hasta alcanzar el total de la oleada
                if (waveActive && timer >= timeBetweenSpawn && zombiesSpawned < totalZombiesToSpawn)
                {
                    SpawnZombie();
                    timer = 0;
                }
                
                // Termina la oleada solo si se han spawneado todos los zombies y ya no hay ninguno activo
                if (zombiesSpawned >= totalZombiesToSpawn && GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && waveActive)
                {
                    EndWave();
                }
            }
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount < 2 && matchStarted)
        {
            Debug.Log("Un jugador se desconectó!");
        }
    }

    private void StartNewWave()
    {
        waveActive = true;
        zombiesSpawned = 0;

        // Aumenta el total de zombies en un 20% respecto a la oleada anterior
        if (waveNumber > 1)
        {
            totalZombiesToSpawn = Mathf.CeilToInt(totalZombiesToSpawn * 1.2f);
        }
        if (waveNumber % 2 == 0)
        {
            IncreaseZombieStats(0.05f); // Incremento del 5%
        }
        // Calcula la cantidad exacta de cada tipo de zombie
        normalZombies = Mathf.FloorToInt(totalZombiesToSpawn * 0.45f);
        fastZombies = Mathf.FloorToInt(totalZombiesToSpawn * 0.35f);
        tankZombies = totalZombiesToSpawn - normalZombies - fastZombies;

        Debug.Log($"Iniciando oleada {waveNumber} con {totalZombiesToSpawn} zombies: {normalZombies} normales, {fastZombies} rápidos, {tankZombies} tanques.");
    }
    private void IncreaseZombieStats(float percentage)
    {
        // Accede a los ScriptableObjects y aumenta sus estadísticas
        IncreaseStats(normalZombiePrefab.GetComponent<EnemyController>().zombieStats, percentage);
        IncreaseStats(fastZombiePrefab.GetComponent<EnemyController>().zombieStats, percentage);
        IncreaseStats(tankZombiePrefab.GetComponent<EnemyController>().zombieStats, percentage);

        Debug.Log($"Estadísticas incrementadas en un {percentage * 100}% para todos los tipos de zombies.");
    }
    private void IncreaseStats(ZombieStatsSO stats, float percentage)
    {
        stats.speed += stats.speed * percentage;
        stats.health = Mathf.CeilToInt(stats.health * (1 + percentage));
        stats.damage = Mathf.CeilToInt(stats.damage * (1 + percentage));
        stats.scoreValue = Mathf.CeilToInt(stats.scoreValue * (1 + percentage));
    }
    private void EndWave()
    {
        waveActive = false;
        waveNumber++;
        timer = -timeBetweenWaves; // Espera entre oleadas
        Debug.Log($"Oleada {waveNumber} completada.");
    }

    private void SpawnZombie()
    {
        GameObject zombieToSpawn = null;

        if (normalZombies > 0)
        {
            zombieToSpawn = normalZombiePrefab;
            normalZombies--;
        }
        else if (fastZombies > 0)
        {
            zombieToSpawn = fastZombiePrefab;
            fastZombies--;
        }
        else if (tankZombies > 0)
        {
            zombieToSpawn = tankZombiePrefab;
            tankZombies--;
        }

        if (zombieToSpawn != null && spawnPoints.Length > 0)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            PhotonNetwork.Instantiate(zombieToSpawn.name, spawnPoint.position, Quaternion.identity);
            zombiesSpawned++;
        }
    }
}
