using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pistol : Guns
{
    public override void Shoot()
    {
        if (Time.time > lastFire + FireDelay)
        {
            GameObject projectileObj = PhotonNetwork.Instantiate(Projectile.name, _firePoint.position, transform.rotation);
            Projectile projectile = projectileObj.GetComponent<Projectile>();

            projectile.LaunchProjectile(transform.right);
            projectile.SetOwner(this);

            lastFire = Time.time;
        }
    }
}
