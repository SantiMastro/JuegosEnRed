using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int ammoValue;

    [SerializeField] private bool isPistol;
    [SerializeField] private bool isShotgun;
    [SerializeField] private bool isUzi;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PhotonView ammoPhotonView = GetComponent<PhotonView>();
            PhotonView playerPhotonView = collision.gameObject.GetComponent<PhotonView>();

            ammoPhotonView.RPC("CollectAmmo", RpcTarget.AllBuffered, ammoPhotonView.ViewID, playerPhotonView.ViewID);
        }
    }

    [PunRPC]
    void CollectAmmo(int ammoViewID, int playerViewID)
    {
        PhotonView ammoPhotonView = PhotonView.Find(ammoViewID);

        if (ammoPhotonView != null)
        {
            PhotonView playerPhotonView = PhotonView.Find(playerViewID);

            if (ammoPhotonView.IsMine)
            {
                PhotonNetwork.Destroy(ammoPhotonView.gameObject);
            }

            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                if (isPistol)
                {
                    StatsManager.instance.AddPistolAmmoToPool(ammoValue);
                }
                if (isShotgun)
                {
                    StatsManager.instance.AddShotgunAmmoToPool(ammoValue);
                }
                if (isUzi)
                {
                    StatsManager.instance.AddUziAmmoToPool(ammoValue);
                }
            }
        }
    }
}
