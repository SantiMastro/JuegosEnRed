using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float timeToStartSpawning;
    [SerializeField] private float timeBetweenSpawn;

    private bool readyToSpawn;
    private float timer;
    private bool matchStarted = false;  

    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount >= 2) //if el cual se encarga de verificar que empiecen a spawn de enemigos cuando los players sean igual o mayor a 2
        {
            matchStarted = true;
            if (PhotonNetwork.IsMasterClient)
            {
                timer += Time.deltaTime;
                SpawnEnemies();
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
