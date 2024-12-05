using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    private GameObject _player;
    private PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        _player = PhotonNetwork.Instantiate(_playerPrefab.name, new Vector2(Random.Range(52, 58), Random.Range(11, 6)), Quaternion.identity, 0, new object[] { PhotonNetwork.LocalPlayer });
        PhotonNetwork.LocalPlayer.TagObject = _player;
    }

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
