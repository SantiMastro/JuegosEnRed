using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawn : MonoBehaviour
{
    //[SerializeField] private GameObject _enemyPrefab;
    //[SerializeField] private float timeToStartSpawning;
    //[SerializeField] private float timeBetweenSpawn;

    //private bool readyToSpawn;
    //private float timer;
    //private bool matchStarted = false;  

    //private void Start()
    //{
    //    timer = 0;
    //}

    //private void Update()
    //{
    //    if(PhotonNetwork.CurrentRoom.PlayerCount >= 2) //if el cual se encarga de verificar que empiecen a spawn de enemigos cuando los players sean igual o mayor a 2
    //    {
    //        matchStarted = true;
    //        if (PhotonNetwork.IsMasterClient)
    //        {
    //            timer += Time.deltaTime;
    //            SpawnEnemies();
    //        }
    //    }
    //    else if (PhotonNetwork.CurrentRoom.PlayerCount < 2 && matchStarted) //if de cuando la cantidad de jugadores es la requerida y la partida empezó, cuando alguien se desconecta mandará un mensaje de error.
    //    {
    //        Debug.Log("Un jugador se desconectó!");
    //    }
    //}

    //private void SpawnEnemies()
    //{
    //    if (!readyToSpawn && timeToStartSpawning < timer)
    //    {
    //        readyToSpawn = true;
    //        timer = 0;
    //    }

    //    if (readyToSpawn && timer > timeBetweenSpawn)
    //    {
    //        timer = 0;
    //        PhotonNetwork.Instantiate(_enemyPrefab.name, new Vector2(Random.Range(4, -4), Random.Range(4, -4)), Quaternion.identity);
    //    }
    //}
    [SerializeField] private GameObject normalZombiePrefab;
    [SerializeField] private GameObject fastZombiePrefab;
    [SerializeField] private GameObject tankZombiePrefab;

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

    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 1)
        {
            matchStarted = true;

            if (PhotonNetwork.IsMasterClient)
            {
                timer += Time.deltaTime;

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

        // Calcula la cantidad exacta de cada tipo de zombie
        normalZombies = Mathf.FloorToInt(totalZombiesToSpawn * 0.45f);
        fastZombies = Mathf.FloorToInt(totalZombiesToSpawn * 0.35f);
        tankZombies = totalZombiesToSpawn - normalZombies - fastZombies;

        Debug.Log($"Iniciando oleada {waveNumber} con {totalZombiesToSpawn} zombies: {normalZombies} normales, {fastZombies} rápidos, {tankZombies} tanques.");
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

        if (zombieToSpawn != null)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-4f, 4f), Random.Range(-4f, 4f));
            PhotonNetwork.Instantiate(zombieToSpawn.name, spawnPosition, Quaternion.identity);
            zombiesSpawned++;
        }
    }
}
