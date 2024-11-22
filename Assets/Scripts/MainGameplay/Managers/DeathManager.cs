using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public GameObject[] players;

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
            GoToMainMenu();
        }
    }

    private void GoToMainMenu()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            players = GameObject.FindGameObjectsWithTag("Player");

            CheckAllPlayersDead();
        }
    }
}
