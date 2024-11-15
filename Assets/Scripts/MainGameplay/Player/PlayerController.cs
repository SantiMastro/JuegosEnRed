using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _speed = 5;
    private PhotonView pv;
    private Camera _camera;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pv.IsMine && collision.transform.CompareTag("Coin"))
        {
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("CollectCoin", RpcTarget.AllBuffered, collision.gameObject.GetComponent<PhotonView>().ViewID);
        }
    }

    [PunRPC]
    
    void CollectCoin(int coinViewID)
    {
         PhotonView coinPhotonView = PhotonView.Find(coinViewID);
         if (coinPhotonView != null)
        {
            PhotonNetwork.Destroy(coinPhotonView.gameObject);
            if (pv.IsMine)
            {
                GameManager.instance.AddCoinToPool();
            }
         }
    }
}
