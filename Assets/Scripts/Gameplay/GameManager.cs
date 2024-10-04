using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
   

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnPlayerDeath(PlayerController deadPlayer)
    {
        PlayerController otherPlayer = GetOtherPlayer(deadPlayer);
        if (otherPlayer != null && !otherPlayer.isDead)
        {
            StartCoroutine(RespawnCountdown(deadPlayer));
        }
    }

    PlayerController GetOtherPlayer(PlayerController currentPlayer)
    {
        foreach (var player in FindObjectsOfType<PlayerController>())
        {
            if (player != currentPlayer && !player.isDead)
            {
                return player;
            }
        }
        return null;
    }

    IEnumerator RespawnCountdown(PlayerController deadPlayer)
    {
        yield return new WaitForSeconds(10);

        PlayerController otherPlayer = GetOtherPlayer(deadPlayer);
        if (otherPlayer != null && !otherPlayer.isDead)
        {
            photonView.RPC("RespawnPlayer", RpcTarget.AllBuffered, deadPlayer.pv.ViewID);
        }
    }
    [PunRPC]
    void RespawnPlayer(int playerID)
    {
        PhotonView playerView = PhotonView.Find(playerID);
        PlayerController player = playerView.GetComponent<PlayerController>();
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.isDead = false;
    }
   
}
