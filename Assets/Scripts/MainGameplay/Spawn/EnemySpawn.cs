using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float timeToStartSpawning;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private GameObject textGameStart;
    [SerializeField] private GameObject waitingPlayers;

    private bool readyToSpawn;
    private float timer;
    private float timerRunTheWave;
    private bool matchStarted = false;

    //private float countdownTime = 10f; // Tiempo de cuenta regresiva
    //private bool isCountdownActive = false; // Para activar el temporizador
    //private bool hasSpawnedEnemies = false; // Controla si los enemigos ya han sido generados       

    private void Start()
    {
        timer = 0;
        timerRunTheWave = 0;
        waitingPlayers.SetActive(true);
        textGameStart.SetActive(false);
    }

    private void Update()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount >= 2) //if el cual se encarga de verificar que empiecen a spawn de enemigos cuando los players sean igual o mayor a 2
        {
            matchStarted = true;
            waitingPlayers.SetActive(false);
            textGameStart.SetActive(true);

            timer += Time.deltaTime;
            timerRunTheWave += Time.deltaTime;

            if (PhotonNetwork.IsMasterClient)
            {
                if (timerRunTheWave >= 10)
                {
                    textGameStart.SetActive(false);
                    timerRunTheWave = 15;
                    SpawnEnemies();
                }
            }
            if (timerRunTheWave >= 10)
            {
                textGameStart.SetActive(false);
            }
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount < 2 && matchStarted) //if de cuando la cantidad de jugadores es la requerida y la partida empezó, cuando alguien se desconecta mandará un mensaje de error.
        {
            Debug.Log("Un jugador se desconectó!");
        }
    }

    private void SpawnEnemies()
    {
        if (!readyToSpawn && timeToStartSpawning < timer)
        {
            readyToSpawn = true;
            timer = 0;
        }

        if (readyToSpawn && timer > timeBetweenSpawn)
        {
            timer = 0;
            PhotonNetwork.Instantiate(_enemyPrefab.name, new Vector2(Random.Range(4, -4), Random.Range(4, -4)), Quaternion.identity);
        }
    }
}
