using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shotgun : Guns
{
    [SerializeField] private int _bulletPerShell = 5;

    public override void Shoot()
    {
        if (StatsManager.instance.totalShotgunAmmo > 0 && Time.time > lastFire + FireDelay)
        {
            for (int i = 0; i < _bulletPerShell; i++)
            {
                GameObject projectileObj = PhotonNetwork.Instantiate(Projectile.name, _firePoint.position + Random.insideUnitSphere * 1, Quaternion.identity);
                Projectile projectile = projectileObj.GetComponent<Projectile>();

                projectile.LaunchProjectile(transform.right);
                projectile.SetOwner(this);

                lastFire = Time.time;
            }
            StatsManager.instance.AddShotgunAmmoToPool(-1);
        }
        else
        {
            Debug.Log("No hay balas");
        }
    }
}
