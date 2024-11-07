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

    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            timer += Time.deltaTime;
            SpawnEnemies();
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
