using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour, IProjectile
{
    public float Speed => _speed;
    public float LifeTime => _lifeTime;
    public LayerMask HitteableLayer => _hitteableLater;
    public IGuns Owner => _owner;

    [SerializeField] protected float _speed = 7f;
    [SerializeField] protected float _lifeTime = 2f;
    [SerializeField] protected LayerMask _hitteableLater;
    [SerializeField] protected IGuns _owner;

    private Rigidbody2D _projectileRb;
    private Collider2D _collider;

    private void Awake()
    {
        _projectileRb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        Init();
    }

    public void Init()
    {
        _collider.isTrigger = true;
        _projectileRb.isKinematic = true;
        _projectileRb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    public void LaunchProjectile(Vector2 direction)
    {
        _projectileRb.velocity = direction * _speed;
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(((1<<other.gameObject.layer) & _hitteableLater) != 0)
        {
            Debug.Log("Da�o!");
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public void SetOwner(IGuns guns) => _owner = guns;
}