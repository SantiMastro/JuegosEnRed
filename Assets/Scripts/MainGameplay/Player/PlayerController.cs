using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _speed = 5;
    private PhotonView pv;
    private Camera _camera;
    private int totalCoins = 0; // Total de monedas recogidas

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        _camera = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        _camera.gameObject.SetActive(pv.IsMine);
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            Move();
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * _speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -Vector3.up * _speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -Vector3.right * _speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * _speed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pv.IsMine && other.CompareTag("Coin"))
        {
            Coin coin = other.GetComponent<Coin>();
            if (coin != null)
            {
                totalCoins += coin.GetValue();
                PhotonNetwork.Destroy(other.gameObject); // Destruir la moneda en la red
                Debug.Log("Monedas totales: " + totalCoins);
            }
        }
    }
}
