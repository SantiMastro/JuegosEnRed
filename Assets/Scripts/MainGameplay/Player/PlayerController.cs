using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviour, IDamageable
{
    public int MaxHealt => _maxHealth;
    public int CurrentHealth => _currentHealth;

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _speed = 5;
    [SerializeField] TMPro.TextMeshPro _nicknamePlayer;
    [SerializeField] private IGuns _guns;
    [SerializeField] List<Guns> _gunsList;

    public int _currentHealth;
    private PhotonView pv;
    private Camera _camera;
    private Animator _animator;
    private float timerRunTheWave;
    private bool hasTeleported = false;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        _camera = GetComponentInChildren<Camera>();
        _animator = GetComponent<Animator>();
        _guns = _gunsList[0];

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
        _currentHealth = MaxHealt;
        _camera.gameObject.SetActive(pv.IsMine);
    }

    private void Update()
    {
        Debug.Log(_currentHealth + name);

        if (pv.IsMine)
        {
            Move();
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                timerRunTheWave += Time.deltaTime;

                if (timerRunTheWave >= 10 && !hasTeleported)
                {
                    TeleportAllPlayers();
                    hasTeleported = true;
                    timerRunTheWave = 0;
                }
            }
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
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _guns.Shoot();
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SwitchGuns(0);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            SwitchGuns(1);
        }
        _animator.SetInteger("MovementDirection", direction);
    }

    private void SwitchGuns(int index)
    {
        foreach (Guns guns in _gunsList)
        {
            guns.gameObject.SetActive(false);
        }

        _gunsList[index].gameObject.SetActive(true);
        _guns = _gunsList[index];

        pv.RPC("Switch", RpcTarget.AllBuffered, index);
    }

    public void TakeDamage(int damage)
    {
        if (pv.IsMine)
        {
            _currentHealth -= damage;
            Debug.Log($"{_currentHealth} + {name}");
            StatsManager.instance.UpdateHealth(_currentHealth, _maxHealth);

            if (_currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        _animator.SetBool("IsDead", true);
    }

    private void TeleportAllPlayers()
    {
         Vector2 targetPosition = new Vector2(Random.Range(-4, 4), Random.Range(-4, 4));
         pv.RPC("Teleport", RpcTarget.AllBuffered, targetPosition);
    }

    [PunRPC]
    public void Teleport(Vector2 newPosition)
    {
        transform.position = newPosition;
    }
    [PunRPC]
    public void Switch(int index)
    {
        foreach (Guns guns in _gunsList)
        {
            guns.gameObject.SetActive(false);
        }
        _gunsList[index].gameObject.SetActive(true);
        _guns = _gunsList[index];
    }
}
