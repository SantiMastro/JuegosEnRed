using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    private void Start()
    {
        PhotonNetwork.Instantiate(_playerPrefab.name, new Vector2(Random.Range(4, -4), Random.Range(4, -4)) , Quaternion.identity);
    }
}
    