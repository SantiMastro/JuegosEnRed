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
    [SerializeField] public bool isDead { get; private set; }

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        _camera = GetComponentInChildren<Camera>();
        _animator = GetComponent<Animator>();
        _guns = _gunsList[0];

        if (pv.IsMine)
        {
            _nicknamePlayer.text = PhotonNetwork.NickName.ToString();
        }
        else
        {
            _nicknamePlayer.text = pv.Owner.NickName.ToString();
        }
    }

    private void Start()
    {
        _currentHealth = MaxHealt;
        _camera.gameObject.SetActive(pv.IsMine);
    }

    private void Update()
    {
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

        if (Input.GetKey(KeyCode.W) && isDead == false)
        {
            transform.position += Vector3.up * _speed * Time.deltaTime;
            direction = 1;
        }
        if (Input.GetKey(KeyCode.S) && isDead == false)
        {
            transform.position += -Vector3.up * _speed * Time.deltaTime;
            direction = -1;
        }
        if (Input.GetKey(KeyCode.A) && isDead == false)
        {
            transform.position += -Vector3.right * _speed * Time.deltaTime;
            direction = -2;
        }
        if (Input.GetKey(KeyCode.D) && isDead == false)
        {
            transform.position += Vector3.right * _speed * Time.deltaTime;
            direction = 2;
        }
        if (Input.GetKey(KeyCode.Mouse0) && isDead == false)
        {
            _guns.Shoot();
        }
        if (Input.GetKey(KeyCode.Alpha1) && isDead == false)
        {
            SwitchGuns(0);
        }
        if (Input.GetKey(KeyCode.Alpha2) && isDead == false)
        {
            SwitchGuns(1);
        }
        if (Input.GetKey(KeyCode.Alpha3) && isDead == false)
        {
            SwitchGuns(2);
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
            StatsManager.instance.UpdateHealth(_currentHealth, _maxHealth);

            if (_currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        if (pv.IsMine)
        {
            StartCoroutine(respawn());
        }
        pv.RPC("SetPlayerState", RpcTarget.AllBuffered, true);
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(20);
        _currentHealth = 20;
        StatsManager.instance.UpdateHealth(_currentHealth, _maxHealth);

        pv.RPC("SetPlayerState", RpcTarget.AllBuffered, false);
    }

    private void TeleportAllPlayers()
    {
        Vector2 targetPosition = new Vector2(Random.Range(-4, 4), Random.Range(-4, 4));
        pv.RPC("Teleport", RpcTarget.AllBuffered, targetPosition);
    }

    [PunRPC]
    public void SetPlayerState(bool isDeadState)
    {
        if (isDeadState)
        {
            gameObject.tag = "Untagged";
            isDead = true;
            _speed = 0;
            var collider = pv.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            _animator.SetBool("IsDead", true);
        }
        else
        {
            gameObject.tag = "Player";
            isDead = false;
            _speed = 5;
            var collider = pv.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = true;
            }
            _animator.SetBool("IsDead", false);
        }
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
