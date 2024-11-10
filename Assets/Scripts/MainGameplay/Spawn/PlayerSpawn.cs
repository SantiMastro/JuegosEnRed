using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    GameObject _player;
    private PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        _player = PhotonNetwork.Instantiate(_playerPrefab.name, new Vector2(Random.Range(4, -4), Random.Range(4, -4)), Quaternion.identity);

        //int playerIndex = PhotonNetwork.PlayerList.Length;

        //pv.RPC("ChangeColor", RpcTarget.AllBuffered, _player.GetComponent<PhotonView>().ViewID, playerIndex);
    }

    //[PunRPC]
    //private void ChangeColor(int playerViewID, int playerIndex)
    //{
    //    PhotonView targetPhotonView = PhotonView.Find(playerViewID);

    //    if (targetPhotonView != null ) 
    //    {
    //        targetPhotonView.gameObject.GetComponent<SpriteRenderer>().color = (playerIndex == 1) ? Color.red : Color.blue;
    //    }
    //}
}
