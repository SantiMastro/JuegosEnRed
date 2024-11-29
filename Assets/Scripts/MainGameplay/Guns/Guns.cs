using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour, IGuns
{
    public Projectile Projectile => _gunsStats.ProjectilePrefab;

    public int Damage => _gunsStats.Damage;

    public float FireDelay => _gunsStats.FireDelay;

    [SerializeField] protected GunsStats _gunsStats;
    [SerializeField] protected Transform _firePoint;

    public Transform player;
    public Transform gun;
    protected float lastFire;

    private Camera cam;

    void Start()
    {   
        if (player != null)
        {
            cam = player.GetComponentInChildren<Camera>();
        }
    }

    void Update()
    {
        PointShoot();
    }

    public void PointShoot()
    {
        if (cam != null)
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3 direction = mousePosition - gun.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gun.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public virtual void Shoot() => Debug.Log("Disparo");
}
