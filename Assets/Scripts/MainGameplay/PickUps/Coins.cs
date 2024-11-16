using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Coins : MonoBehaviour
{
    [SerializeField] private int coinValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PhotonView coinPhotonView = GetComponent<PhotonView>();
            PhotonView playerPhotonView = collision.gameObject.GetComponent<PhotonView>();

            coinPhotonView.RPC("CollectCoin", RpcTarget.AllBuffered, coinPhotonView.ViewID, playerPhotonView.ViewID);
        }
    }

    [PunRPC]
    void CollectCoin(int coinViewID, int playerViewID)
    {
        PhotonView coinPhotonView = PhotonView.Find(coinViewID);

        if (coinPhotonView != null)
        {
            PhotonNetwork.Destroy(coinPhotonView.gameObject);
            PhotonView playerPhotonView = PhotonView.Find(playerViewID);

            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                GameManager.instance.AddCoinToPool(coinValue);
            }
        }
    }
}