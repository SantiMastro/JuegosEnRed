using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public GameObject[] players;
    public GameObject FinalScoreMenu;
    public StatsManager _statsManager;

    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void CheckAllPlayersDead()
    {
        bool allDead = true;

        foreach (GameObject player in players)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null && !playerController.isDead)
            {
                allDead = false;
                break;
            }
        }

        if (allDead)
        {
            photonView.RPC("FinalScoreActive", RpcTarget.AllBuffered, true);
        }
    }

    private void GoToMainMenu()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("MainMenu");
        }  
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            players = GameObject.FindGameObjectsWithTag("Player");

            CheckAllPlayersDead();
        }
    }

    private IEnumerator DisconnectToServer()
    {
        yield return new WaitForSeconds(5f);

        GoToMainMenu();
    }

    [PunRPC]
    public void FinalScoreActive(bool isActive)
    {
        _statsManager.UpdateFinalHighscore();
        FinalScoreMenu.SetActive(isActive);
        StartCoroutine(DisconnectToServer());
    }
}
