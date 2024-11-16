using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _speed = 5;
    [SerializeField] TMPro.TextMeshPro _nicknamePlayer;
    private PhotonView pv;
    private Camera _camera;
    private Animator _animator;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        _camera = GetComponentInChildren<Camera>();
        _animator = GetComponent<Animator>();

        if (pv.IsMine)
        {
            _nicknamePlayer.text = PhotonNetwork.NickName.ToString();
            Debug.Log(PhotonNetwork.NickName);
        }
        else
        {
            _nicknamePlayer.text = pv.Owner.NickName.ToString();
            Debug.Log(pv.Owner.NickName);
        }
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
        int direction = 0;

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * _speed * Time.deltaTime;
            direction = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -Vector3.up * _speed * Time.deltaTime;
            direction = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -Vector3.right * _speed * Time.deltaTime;
            direction = -2;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * _speed * Time.deltaTime;
            direction = 2;
        }
        _animator.SetInteger("MovementDirection", direction);
    }
}
