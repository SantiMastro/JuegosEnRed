using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class EnemyController : MonoBehaviourPun
{
   /* public float moveSpeed = 3f;*/ // Velocidad de movimiento del enemigo
    public int health = 100;     // Salud del enemigo
    public float speed = 3f;
    private Transform target;

   
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Encuentra al jugador más cercano u objetivo predeterminado
            target = FindClosestPlayer().transform;
        }
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient && target != null)
        {
            // Mover enemigo hacia el jugador
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }

    GameObject FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject player in players)
        {
            float dist = Vector3.Distance(player.transform.position, currentPos);
            if (dist < minDist)
            {
                closest = player;
                minDist = dist;
            }
        }

        return closest;
    }
    // Método para aplicar daño al enemigo
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Aquí puedes añadir efectos de muerte, animaciones, etc.
        Destroy(gameObject); // Destruye el enemigo
    }
}
