using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzi : Guns
{
    public override void Shoot()
    {
        if (StatsManager.instance.totalUziAmmo > 0 && Time.time > lastFire + FireDelay)
        {
            GameObject projectileObj = PhotonNetwork.Instantiate(Projectile.name, _firePoint.position, transform.rotation);
            Projectile projectile = projectileObj.GetComponent<Projectile>();

            projectile.LaunchProjectile(transform.right);
            projectile.SetOwner(this);

            lastFire = Time.time;
            StatsManager.instance.AddUziAmmoToPool(-1);
        }
    }
}
