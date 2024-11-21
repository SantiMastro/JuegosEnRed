using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Guns
{
    [SerializeField] private int _bulletPerShell = 5;
    public override void Shoot()
    {
        if (Time.time > lastFire + FireDelay)
        {
            for (int i = 0; i < _bulletPerShell; i++)
            {
                Projectile projectile = Instantiate(Projectile, _firePoint.position + Random.insideUnitSphere * 1, Quaternion.identity);
                projectile.LaunchProjectile(transform.right);
                projectile.SetOwner(this);

                lastFire = Time.time;
            }
        }
    }
}
